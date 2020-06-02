using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Tests
{
    /// <summary>
    /// The test class for the <see cref="ViewModelHelpers"/> class and the <see cref="Extensions"/> class
    /// </summary>
    [TestFixture]
    public class ViewModelHelpersTests
    {
        /// <summary>
        /// The list of <see cref="UserMockup"/> to be used through the tests
        /// </summary>
        public List<UserMockup> ListOfMockupUsers { get; set; }

        /// <summary>
        /// The setup to initialize all common objects in the tests
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // Creating a list of mockup users
            ListOfMockupUsers = new List<UserMockup>()
            {
                new UserMockup(){ personalNumber = "0", firstName = "b" },
                new UserMockup(){ personalNumber = "1", firstName = "a" }
            };
        }

        /// <summary>
        /// Test to check if a user with no loans can be removed from the system
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task CanDeleteUser_UserWithNoLoanedArticles_ReturnsTrue()
        {
            // Get the result with a dummy user
            var result = await IoC.CreateInstance<TableControlViewModel>().CanDeleteUser("0000000000");

            // Check the result
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test to to convert a list of <see cref="User"/> to <see cref="UserViewModel"/>
        /// </summary>
        [Test]
        public void FillWithModelData_UserToUserViewModel_ReturnsListOfUserViewModelObjects()
        {
            // Get the result
            var result = ViewModelHelpers.FillWithModelData<IUser, UserViewModel>(ListOfMockupUsers);

            // Check the result
            Assert.That(result, Is.TypeOf(typeof(List<UserViewModel>)));
        }

        /// <summary>
        /// Test to see if a list is converted to an observable collection
        /// </summary>
        [Test]
        public void ToObservableCollection_SendNormalList_ReturnsAsObservableCollection()
        
        {   
            // Get the result
            var result = ListOfMockupUsers.ToObservableCollection();

            // Check the result
            Assert.That(result, Is.TypeOf(typeof(ObservableCollection<UserMockup>)));
        }

        /// <summary>
        /// Test to see if a list can be filled with placeholder items
        /// </summary>
        [Test]
        public void FillPlaceHolders_FillExistingList_NumberOfObjectsInListChanges()
        {
            // Get the result
            var result = ListOfMockupUsers.FillPlaceHolders(8);

            // Checkthe result
            Assert.That(result.Count, Is.EqualTo(8));
        }

        /// <summary>
        /// Test if the sorting works
        /// </summary>
        [Test]
        public void SortByPropertyName_SortByFirstName_SecondUserShouldBeReturned()
        {
            // Get the result
            var result = ListOfMockupUsers.SortByPropertyName("firstName").First();

            // Check the result
            Assert.That(result.firstName, Is.EqualTo("a"));
        }

        /// <summary>
        /// Test to check the possibility to convert a single item that is derived from the same interface
        /// </summary>
        [Test]
        public void ToModel_FromUserToUserViewModel_ReturnsObjectAsUserViewModel()
        {
            // Get the result
            var result = ListOfMockupUsers.First().ToModel<IUser, UserViewModel>();

            // Check the result
            Assert.That(result, Is.TypeOf(typeof(UserViewModel)));
        }

    }
}
