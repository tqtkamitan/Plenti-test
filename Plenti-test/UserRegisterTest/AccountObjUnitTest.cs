using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using UserClass;
//using Xunit;

namespace UserRegisterTest
{
    [TestClass]
    public class AccountObjUnitTest
    {
        private static List<Address> _addressSources = new List<Address>(){
                             new Address("Level 3, 51 Pitt Street", "Sydney", "NSW 2000", (decimal)16.78292562571503, (decimal)126.6265191688881),
                             new Address("Level 3, 51 Pitt Street", "Sydney", "NSW 2000", (decimal)15.78292562571503, (decimal)6.6265191688881),
                             new Address("Level 3, 51 Pitt Street", "Sydney", "NSW 2000", (decimal)32.78292562571503, (decimal)16.6265191688881),
                             new Address("Level 3, 51 Pitt Street", "Sydney", "NSW 2000", (decimal)10.78292562571503, (decimal)106.6265191688881),
        };

        //A2C1B3
        //AB21C3
        private List<User> _userSources = new List<User>(){
                             new User(_addressSources[0], "Tan", "ABC123"),
                             new User(_addressSources[1], "Tan1", "ABC1234"),
                             new User(_addressSources[2], "Tan2", "A2C1B3"),
                             new User(_addressSources[3], "Tan3", "ABC1236"),
        };

        private List<User> _existingUserSources = new List<User>();
        [TestMethod]
        public void Resister()
        {
            var user = new User();

            List<bool> results = new List<bool>();
            foreach (var item in _userSources)
            {
                if (_existingUserSources.Count == 0)
                {
                    results.Add(user.IsMatch(item, new User()));
                    _existingUserSources.Add(item);
                }
                else
                {
                    bool result = true;
                    foreach (var existingItem in _existingUserSources)
                    {
                        if (!user.IsMatch(item, existingItem))
                        {
                            result = user.IsMatch(item, existingItem);
                        }
                    }
                    results.Add(result);
                    _existingUserSources.Add(item);
                }

            }

            Assert.IsTrue(!results.Exists(r => r == false));
        }
    }
}
