using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Virtual_Librarian;
using VirtualLibrarian.API.Core;
using VirtualLibrarian.Domain;

namespace VirtualLibrarianTests
{
    [TestClass]
    public class UserFileParsersTests
    {
        private static readonly string mockUserFileContents = 
            "1,ciuvakas,ciuvakenas,MIF\r\n2,darviens,darvienauskas,KF";

        [TestMethod]
        public void ParseUsersListLength()
        {
            List<User> expectedUsers = new List<User>
            {
                new User(1, "ciuvakas", "ciuvakenas", User.Faculty.MIF),
                new User(2, "darviens", "darvienauskas", User.Faculty.KF)
            };
            List<User> users = FileReaderWriter.Instance.ParseUsers(mockUserFileContents);
            Assert.AreEqual(expectedUsers.Count, users.Count);
        }

        public void ParseUserEquals()
        {
            User expectedUser = new User(2, "darviens", "darvienauskas", User.Faculty.KF);
            User user = FileReaderWriter.Instance.ParseUser(mockUserFileContents, 2);
            Assert.AreEqual(expectedUser, user);
        }
    }
}
