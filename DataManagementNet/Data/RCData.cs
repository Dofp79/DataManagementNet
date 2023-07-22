using DataManagementNet.Models;
using System.Data.SqlClient;
using System.Data;
using DataManagementNet.Datos;

namespace DataManagementNet.Data
{
    /// <summary>
    /// The RCData class is responsible for the functions to read, store, modify, search and delete data in the database. It uses 
    /// SQL commands and connection instances in order to activate the procedure previously defined in the same database corresponding to the desired function.
    /// </summary>
    public class RCData
    {
        /// <summary>
        /// In summary, this method establishes a connection to the database, executes the stored 
        /// procedure "sp_ShowContactList" to retrieve contact records, and stores the result in a List of RCModel objects. 
        /// </summary>
        /// <returns></returns>
        public List<RCModel> ListContacts()
        {
            // Create a new List to store RCModel objects
            var objList = new List<RCModel>();

            // Create a new instance of Conexion class to manage the connection string
            var cn = new Conexion();

            // Using block to manage the SQL connection
            using (var conexion = new SqlConnection(cn.getStringSQL()))
            {
                conexion.Open(); // Open the SQL connection

                // Create a new SqlCommand with the name of the stored procedure "sp_ShowContactList"
                SqlCommand cmd = new SqlCommand("sp_ShowContactList", conexion);

                cmd.CommandType = CommandType.StoredProcedure; // Set the command type to stored procedure

                // Using block to manage the SqlDataReader to read data from the SQL command
                using (var dr = cmd.ExecuteReader())
                {
                    // Loop through each row in the SqlDataReader
                    while (dr.Read())
                    {
                        // Create a new RCModel object and set its properties from the data in the SqlDataReader
                        objList.Add(new RCModel()
                        {
                            CID = Convert.ToInt32(dr["CID"]),
                            ContactsName = dr["ContactsName"].ToString(),
                            Phone = dr["Phone"].ToString(),
                            Email = dr["Email"].ToString()
                        });
                    }
                }
            }

            // Return the List of RCModel objects containing the contacts retrieved from the database
            return objList;
        }

        /// <summary>
        ///  this method fetches a specific contact's details from the database using the provided CID, creates an instance of the "RCModel" class to 
        ///  store the contact information, and returns that instance with the retrieved data.
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public RCModel GetContact(int CID)
        {
            var objContact = new RCModel(); // Create an instance of the RCModel class to store the contact information.

            var cn = new Conexion(); // Create an instance of the Conexion class to get the connection string.

            using (var conexion = new SqlConnection(cn.getStringSQL())) // Open a SqlConnection using the connection string.
            {
                conexion.Open(); // Open the connection to the database.

                SqlCommand cmd = new SqlCommand("sp_GetContact", conexion); // Create a SqlCommand with the name of the stored procedure "sp_GetContact."
                cmd.Parameters.AddWithValue("CID", CID); // Add a parameter named "CID" and set its value to the provided CID.
                cmd.CommandType = CommandType.StoredProcedure; // Set the command type to stored procedure.

                using (var dr = cmd.ExecuteReader()) // Execute the SqlCommand and read the data using SqlDataReader.
                {
                    while (dr.Read())
                    {
                        objContact.CID = Convert.ToInt32(dr["CID"]); // Get the CID from the SqlDataReader and set it in the objContact.
                        objContact.ContactsName = dr["ContactsName"].ToString(); // Get the ContactsName from the SqlDataReader and set it in the objContact.
                        objContact.Phone = dr["Phone"].ToString(); // Get the Phone from the SqlDataReader and set it in the objContact.
                        objContact.Email = dr["Email"].ToString(); // Get the Email from the SqlDataReader and set it in the objContact.
                    }
                }
            }

            return objContact; // Return the RCModel object containing the contact information.
        }

        /// <summary>
        /// this method performs the registration of a new contact by inserting the provided contact information into the database using a stored procedure. If the operation is 
        /// successful, it returns true; otherwise, it returns false along with the error message.
        /// </summary>
        /// <param name="ocontacto"></param>
        /// <returns></returns>
        public bool Registration(RCModel ocontacto)
        {
            bool answer; // Variable to store the result of the operation (true for success, false for failure)

            try
            {
                var cn = new Conexion(); // Create an instance of the Conexion class to get the connection string.

                using (var conexion = new SqlConnection(cn.getStringSQL())) // Open a SqlConnection using the connection string.
                {
                    conexion.Open(); // Open the connection to the database.

                    SqlCommand cmd = new SqlCommand("sp_Registration", conexion); // Create a SqlCommand with the name of the stored procedure "sp_Registration."
                    cmd.Parameters.AddWithValue("ContactsName", ocontacto.ContactsName); // Add a parameter named "ContactsName" and set its value from ocontacto.ContactsName.
                    cmd.Parameters.AddWithValue("Phone", ocontacto.Phone); // Add a parameter named "Phone" and set its value from ocontacto.Phone.
                    cmd.Parameters.AddWithValue("Email", ocontacto.Email); // Add a parameter named "Email" and set its value from ocontacto.Email.
                    cmd.CommandType = CommandType.StoredProcedure; // Set the command type to stored procedure.

                    cmd.ExecuteNonQuery(); // Execute the SqlCommand to insert the data into the database.
                }

                answer = true; // If everything was successful, set the answer to true.
            }
            catch (Exception e)
            {
                string error = e.Message; // If an error occurs, store the error message in the "error" variable.
                answer = false; // Set the answer to false to indicate that an error occurred.
            }

            return answer; // Return the answer (true for success, false for failure).
        }


        /// <summary>
        /// this method updates an existing contact's information in the database using the provided "RCModel" object. If the operation is successful, it returns 
        /// true; otherwise, it returns false along with the error message.
        /// </summary>
        /// <param name="objcontact"></param>
        /// <returns></returns>
        public bool EditContact(RCModel objcontact)
        {
            bool answer; // Variable to store the result of the operation (true for success, false for failure)

            try
            {
                var cn = new Conexion(); // Create an instance of the Conexion class to get the connection string.

                using (var conexion = new SqlConnection(cn.getStringSQL())) // Open a SqlConnection using the connection string.
                {
                    conexion.Open(); // Open the connection to the database.

                    SqlCommand cmd = new SqlCommand("sp_EditContact", conexion); // Create a SqlCommand with the name of the stored procedure "sp_EditContact."
                    cmd.Parameters.AddWithValue("CID", objcontact.CID);
                    cmd.Parameters.AddWithValue("ContactsName", objcontact.ContactsName); // Add a parameter named "ContactsName" and set its value from objcontact.ContactsName.
                    cmd.Parameters.AddWithValue("Phone", objcontact.Phone); // Add a parameter named "Phone" and set its value from objcontact.Phone.
                    cmd.Parameters.AddWithValue("Email", objcontact.Email); // Add a parameter named "Email" and set its value from objcontact.Email.
                    cmd.CommandType = CommandType.StoredProcedure; // Set the command type to stored procedure.

                    cmd.ExecuteNonQuery(); // Execute the SqlCommand to update the contact information in the database.
                }

                answer = true; // If everything was successful, set the answer to true.
            }
            catch (Exception e)
            {
                string error = e.Message; // If an error occurs, store the error message in the "error" variable.
                answer = false; // Set the answer to false to indicate that an error occurred.
            }

            return answer; // Return the answer (true for success, false for failure).
        }

        /// <summary>
        ///  this method deletes a contact from the database using the provided Contact ID (CID). If the deletion is successful, it returns 
        ///  true; otherwise, it returns false along with the error message.
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public bool DeleteContat(int CID)
        {
            bool answer; // Variable to store the result of the operation (true for success, false for failure)

            try
            {
                var cn = new Conexion(); // Create an instance of the Conexion class to get the connection string.

                using (var conexion = new SqlConnection(cn.getStringSQL())) // Open a SqlConnection using the connection string.
                {
                    conexion.Open(); // Open the connection to the database.

                    SqlCommand cmd = new SqlCommand("sp_DeleteContat", conexion); // Create a SqlCommand with the name of the stored procedure "sp_DeleteContat."
                    cmd.Parameters.AddWithValue("CID", CID); // Add a parameter named "CID" and set its value to the provided Contact ID.
                    cmd.CommandType = CommandType.StoredProcedure; // Set the command type to stored procedure.

                    cmd.ExecuteNonQuery(); // Execute the SqlCommand to delete the contact from the database.
                }

                answer = true; // If the deletion is successful, set the answer to true.
            }
            catch (Exception e)
            {
                string error = e.Message; // If an error occurs, store the error message in the "error" variable.
                answer = false; // Set the answer to false to indicate that an error occurred.
            }

            return answer; // Return the answer (true for success, false for failure).
        }
    }
}

    