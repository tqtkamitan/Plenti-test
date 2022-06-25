using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace UserClass
{
    public class User : IUserMatch
    {
        public Address Address { get; set; }
        public string Name { get; set; }
        public string ReferralCode { get; set; }

        public User(Address address, string name, string referralCode)
        {
            Address = address;
            Name = name;
            ReferralCode = referralCode;
        }

        public User () {
            Address = new Address();
        }

        public bool IsMatch(User newUser, User existingUser)
        {
            bool isReferralMatch = ReferralCodeMatch(newUser.ReferralCode, existingUser.ReferralCode);
            bool isUserAddressMatch = newUser.Name == existingUser.Name && RemoveUnusalCharacterInAddress(GetAddress(newUser.Address)) == RemoveUnusalCharacterInAddress(GetAddress(existingUser.Address));
            bool isLocationMatch = DistanceBetweenPlaces(newUser.Address, existingUser.Address) <= 500;
            return !isReferralMatch && !isUserAddressMatch && !isLocationMatch;
        }

        const double PIx = Math.PI;
        const double RADIUS = 6378.16;

        public bool ReferralCodeMatch(string userReferralCode, string compareReferralCode)
        {
            if (compareReferralCode == null)
            {
                return false;
            }
            if (userReferralCode == compareReferralCode)
            {
                return true;
            }
            else
            {
                char[] userReferralCodeChar = userReferralCode.ToCharArray();
                char[] compareReferralCodeChar = compareReferralCode.ToCharArray();
                if (userReferralCodeChar.Length == compareReferralCodeChar.Length)
                {
                    if (userReferralCodeChar.SequenceEqual(userReferralCodeChar))
                    {
                        //char[] reversalChar = new char[2];
                        for (int i = 0; i < userReferralCodeChar.Length; i++)
                        {
                            if (userReferralCodeChar[i] != compareReferralCodeChar[i])
                            {
                                if (i + 2 > userReferralCode.Length)
                                    return false;
                                bool isReversalChar = userReferralCodeChar[i+2] == compareReferralCodeChar[i] && userReferralCodeChar[i+1] == compareReferralCodeChar[i+1] && userReferralCodeChar[i] == compareReferralCodeChar[i+2];
                                i = i + 2;
                                if (!isReversalChar)
                                    return false;
                            }
                        }
                        return true;
                    }
                    else { return false; }
                }
                else
                {
                    return false;
                }
            }
        }

        public double Radians(double x)
        {
            return x * PIx / 180;
        }

        public double DistanceBetweenPlaces(Address newAddress, Address existingAddress)
        {
            double dlon = Radians((double)existingAddress.Longitude - (double)newAddress.Longitude);
            double dlat = Radians((double)existingAddress.Latitude - (double)newAddress.Latitude);

            double a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + Math.Cos(Radians((double)newAddress.Latitude)) * Math.Cos(Radians((double)existingAddress.Latitude)) * (Math.Sin(dlon / 2) * Math.Sin(dlon / 2));
            double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return angle * RADIUS;
        }

        public string GetAddress(Address address)
        {
            return address.StreetAddress + ", " + address.Suburb + ", " + address.State;
        }

        public string RemoveUnusalCharacterInAddress(string address)
        {
            var normaliseAddress = address.ToLower();
            normaliseAddress = Regex.Replace(normaliseAddress, "[^a-zA-Z0-9_., -]+", "", RegexOptions.Compiled);
            normaliseAddress = normaliseAddress.Replace("_", " ").Replace("-", " ");
            return normaliseAddress;
        }
    }
}
