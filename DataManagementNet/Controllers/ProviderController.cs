using Microsoft.AspNetCore.Mvc;
using DataManagementNet.Data;
using DataManagementNet.Models;

namespace DataManagementNet.Controllers
{
    public class ProviderController : Controller
    {

        // Create an instance of the RCData class, which provides data access and manipulation methods for the contacts.
        RCData _RCData = new RCData();


        /// <summary>
        /// This action method is intended to handle HTTP GET requests to display a list of contacts.
        /// </summary>
        /// <returns></returns>
        public IActionResult ListContacts()
        {
            // Call the _RCData.ListContacts() method to retrieve a list of contacts.
            // The result is stored in the oList variable.
            var oList = _RCData.ListContacts();

            // Return the "ListContacts" view and pass the oList as the model.
            // This will display the list of contacts on the view.
            return View(oList);
        }


        /// <summary>
        /// This action method is intended to handle HTTP GET requests to display the registration view.
        /// </summary>
        /// <returns></returns>
        public IActionResult Registration()
        {
            // Return the "Registration" view, which is likely a form for users to input contact information for registration.
            return View();
        }



        /// <summary>
        /// This action method is intended to handle HTTP POST requests to register a new contact.
        /// </summary>
        /// <param name="oContact"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Registration(RCModel oContact)
        {
            // Check if the model state is not valid, meaning there are validation errors in the submitted data.
            // If there are validation errors, return the same view with the validation errors displayed to the user.
            if (!ModelState.IsValid)
                return View();

            // Call the _RCData.Registration method to register the new contact.
            // The method will attempt to add the contact information to the data source.
            var aswer = _RCData.Registration(oContact);

            // Check if the registration was successful (aswer is true).
            // If successful, redirect to the "ListContacts" action method, presumably to display the list of contacts.
            // If not successful, return the same view, indicating that there was an error during the registration process.
            if (aswer)
                return RedirectToAction("ListContacts");
            else
                return View();
        }



        /// <summary>
        /// This action method is intended to handle GET requests to edit a contact with the specified CID.
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public IActionResult EditContact(int CID)
        {
            // Call the _RCData.GetContact method to retrieve the contact information for the given CID.
            var ocontact = _RCData.GetContact(CID);

            // Return the View, presumably an edit view, passing the retrieved contact information as the model.
            // The view will use this model to pre-populate the form fields with the existing contact information for editing.
            return View(ocontact);
        }


        /// <summary>
        /// This is an HTTP POST action method, which means it is intended to handle form submissions. It takes a parameter 
        /// 'oContact' of type RCModel, which represents the contact information to be edited.
        /// </summary>
        /// <param name="oContact"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditContact(RCModel oContact)
        {        
            // Check if the model state is valid, meaning the data passed in the 'oContact' object passes validation rules.
            if (!ModelState.IsValid)
            {
                // If the model state is not valid (e.g., some required fields are not provided or have invalid values),
                // return the same View (presumably the edit view) to show the validation errors to the user.
                return View();
            }

            // If the model state is valid, call the _RCData.EditContact method to update the contact information in the database.
            var answer = _RCData.EditContact(oContact);

            // Check if the update operation was successful (answer == true).
            if (answer)
            {
                // If the update was successful, redirect the user to the "ListContacts" action, presumably a list view of all contacts.
                return RedirectToAction("ListContacts");
            }
            else
            {
                // If the update was not successful (answer == false), return the same View (presumably the edit view) to show an error message to the user.
                return View();
            }
        }


        /// <summary>
        /// This is a C# ASP.NET Core controller action that returns an IActionResult.
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public IActionResult DeleteContat(int CID)
        {
            // Call the _RCData.GetContact method to retrieve the contact information based on the provided CID.
            var answer = _RCData.GetContact(CID);

            // Return the View that corresponds to the "DeleteContat" action.
            // It is expected that this View will render the details of the contact that the user wants to delete.
            return View();
        }


        /// <summary>
        /// This is a C# ASP.NET Core controller action with the [HttpPost] attribute, which means it is intended to handle 
        /// HTTP POST requests.
        /// </summary>
        /// <param name="oContact"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteContat(RCModel oContact)
        {
            // Call the DeleteContat method of the _RCData object to delete the contact.
            var answer = _RCData.DeleteContat(oContact.CID);

            // Check if the deletion was successful (answer is true).
            if (answer)
            {
                // If successful, redirect the user to the "ListContacts" action, 
                // likely to display a list of contacts.
                return RedirectToAction("ListContacts");
            }
            else
            {
                // If there was an error in the deletion process, return the same view, 
                // allowing the user to see an error message or handle the error in some way.
                return View();
            }
        }

    }
}
