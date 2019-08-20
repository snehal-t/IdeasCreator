using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ideas.Models;
using System.Data.SqlClient;
using System.Linq;

namespace Ideas.Data.Repositories.Ideas.Imp
{
    public class IdeasRepository : RepositoryBase, IIdeasRepository
    {
        private readonly IDbConnection _dbConnection;
        public IdeasRepository(IDbConnection dbConnection) => _dbConnection = dbConnection;

        public User SignIn(string name, string email)
        {
            User user = new User();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_USER_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Add"));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_NAME", name));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID ", email));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_ROLE", "Contributor"));
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    user = ReturnUser(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return user;
        }

        public InviteeList CreateIdea(Idea idea, string email, string author)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Add"));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", null));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_TYPE", idea.Type));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_NAME", idea.Title));
                sqlCommand.Parameters.Add(new SqlParameter("TITLE", idea.Title));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_SOURCE", idea.Source));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_DESCRIPTION", idea.Description));
                sqlCommand.Parameters.Add(new SqlParameter("BUSINESS_CASE", idea.BusinessCase));
                sqlCommand.Parameters.Add(new SqlParameter("ETA", idea.IdealTime));
                sqlCommand.Parameters.Add(new SqlParameter("ETA_JUSTIFICATION", idea.BusinessJustification));
                sqlCommand.Parameters.Add(new SqlParameter("CONTACT_PERSON", idea.ContactName));
                sqlCommand.Parameters.Add(new SqlParameter("CONTACT_NUMBER", idea.ContactEmail));
                sqlCommand.Parameters.Add(new SqlParameter("CONTACT_EMAIL", idea.ContactMobileNo));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    usersForNotification = ReturnUserList(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return GetInviteeList(usersForNotification);
        }

        public InviteeList UpdateIdea(Idea idea, string email, string author)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Edit"));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", idea.IdeaId));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_TYPE", idea.Type));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_NAME", idea.Name));
                sqlCommand.Parameters.Add(new SqlParameter("TITLE", idea.Title));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_SOURCE", idea.Source));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_DESCRIPTION", idea.Description));
                sqlCommand.Parameters.Add(new SqlParameter("BUSINESS_CASE", idea.BusinessCase));
                sqlCommand.Parameters.Add(new SqlParameter("ETA", idea.IdealTime));
                sqlCommand.Parameters.Add(new SqlParameter("ETA_JUSTIFICATION", idea.BusinessJustification));
                sqlCommand.Parameters.Add(new SqlParameter("CONTACT_PERSON", idea.ContactName));
                sqlCommand.Parameters.Add(new SqlParameter("CONTACT_NUMBER", idea.ContactMobileNo));
                sqlCommand.Parameters.Add(new SqlParameter("CONTACT_EMAIL", idea.ContactEmail));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    usersForNotification = ReturnUserList(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return GetInviteeList(usersForNotification);
        }

        public InviteeList WithdrawIdea(string ideaId, string email, string author, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Withdraw"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    usersForNotification = ReturnUserList(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return GetInviteeList(usersForNotification);
        }

        public InviteeList ApproveIdea(string ideaId, string email, string author, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Approved"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    usersForNotification = ReturnUserList(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return GetInviteeList(usersForNotification);
        }

        public InviteeList RejectIdea(string ideaId, string email, string author, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Reject"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    usersForNotification = ReturnUserList(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return GetInviteeList(usersForNotification);
        }

        public InviteeList DeligateIdea(string ideaId, string assignee, string userComments, string email, string author)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Delegate"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("DELEGATE_APPROVER_EMAIL", assignee));
                sqlCommand.Parameters.Add(new SqlParameter("DELEGATE_APPROVER_NAME", assignee));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    usersForNotification = ReturnUserList(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return GetInviteeList(usersForNotification);
        }

        public InviteeList PickIdea(string ideaId, string email, string author, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Pick"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    usersForNotification = ReturnUserList(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return GetInviteeList(usersForNotification);
        }

        public InviteeList PickIdeaDone(string ideaId, string email, string author, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Done"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    usersForNotification = ReturnUserList(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return GetInviteeList(usersForNotification);
        }

        public InviteeList PickIdeaGiveUp(string ideaId, string email, string author, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "GiveUp"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    usersForNotification = ReturnUserList(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return GetInviteeList(usersForNotification);
        }

        public InviteeList PickIdeaRework(string ideaId, string email, string author, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Rework"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    usersForNotification = ReturnUserList(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return GetInviteeList(usersForNotification);
        }

        public InviteeList PickIdeaAccept(string ideaId, string email, string author, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Accept"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    usersForNotification = ReturnUserList(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return GetInviteeList(usersForNotification);
        }

        public InviteeList PickIdeaReopen(string ideaId, string email, string author, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Reopen"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    usersForNotification = ReturnUserList(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return GetInviteeList(usersForNotification);
        }

        public Response WatchIdea(string ideaId, string email, string author, bool isActive)
        {
            Response response = new Response();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (isActive)
                {
                    sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Watch"));
                }
                else
                {
                    sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Unwatch"));
                }
                
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));
                
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        response.IsSuccess = reader.IsDBNull(reader.GetOrdinal("IS_SUCCESS")) ? false : reader.GetBoolean(reader.GetOrdinal("IS_SUCCESS"));
                        response.Message = reader.IsDBNull(reader.GetOrdinal("DB_MESSAGE")) ? string.Empty : reader.GetString(reader.GetOrdinal("DB_MESSAGE"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return response;
        }

        public InviteeList CommentIdea(string ideaId, string email, string author, bool commentType, long commentParentId, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Comment"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));
                sqlCommand.Parameters.Add(new SqlParameter("PARENT_COMMENT_ID", commentParentId));
                sqlCommand.Parameters.Add(new SqlParameter("IS_COMMENT_PUBLIC", 1));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    usersForNotification = ReturnUserList(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return GetInviteeList(usersForNotification);
        }

        public bool EditComment(string ideaId, string commentId, string email, string author, bool commentType, long commentParentId, string userComments)
        {
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_COMMENT_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Edit"));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("AUTHORIZATION_ID", author));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_ID", commentId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));
                sqlCommand.Parameters.Add(new SqlParameter("IS_COMMENT_PUBLIC", 1));
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return true;
        }

        public bool DeleteComment(string ideaId, string commentId, string email, string author)
        {
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_COMMENT_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Delete"));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("AUTHORIZATION_ID", author));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_ID", commentId));
                sqlCommand.Parameters.Add(new SqlParameter("IS_COMMENT_PUBLIC", 1));
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return true;
        }


        public List<Idea> GetIdeas(string email, string author, string ideaPage, int pageSize, int currentPage, string orderBy, string order)
        {
            List<Idea> ideas = new List<Idea>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_GET_IDEA_LIST]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", ideaPage));
                sqlCommand.Parameters.Add(new SqlParameter("AUTHORIZATION_ID", author));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("Page_Size", pageSize));
                sqlCommand.Parameters.Add(new SqlParameter("Current_Page", currentPage));
                sqlCommand.Parameters.Add(new SqlParameter("Order_By", orderBy));
                sqlCommand.Parameters.Add(new SqlParameter("Sort_Order", order));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Idea idea = new Idea();

                        idea.IdeaId = reader.IsDBNull(reader.GetOrdinal("Idea_Id")) ? null : reader.GetGuid(reader.GetOrdinal("Idea_Id")).ToString();
                        idea.Type = reader.IsDBNull(reader.GetOrdinal("Idea_Type")) ? string.Empty : reader.GetString(reader.GetOrdinal("Idea_Type"));
                        idea.Source = reader.IsDBNull(reader.GetOrdinal("Idea_Source")) ? string.Empty : reader.GetString(reader.GetOrdinal("Idea_Source"));
                        idea.IdeaName = reader.IsDBNull(reader.GetOrdinal("Idea_Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Idea_Name"));
                        idea.Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? string.Empty : reader.GetString(reader.GetOrdinal("Title"));
                        idea.BusinessCase = reader.IsDBNull(reader.GetOrdinal("BUSINESS_CASE")) ? string.Empty : reader.GetString(reader.GetOrdinal("BUSINESS_CASE"));
                        idea.Description = reader.IsDBNull(reader.GetOrdinal("IDEA_DESCRIPTION")) ? string.Empty : reader.GetString(reader.GetOrdinal("IDEA_DESCRIPTION"));
                        idea.IdealTime = reader.IsDBNull(reader.GetOrdinal("ETA")) ? string.Empty : reader.GetString(reader.GetOrdinal("ETA"));
                        idea.BusinessJustification = reader.IsDBNull(reader.GetOrdinal("ETA_JUSTIFICATION")) ? string.Empty : reader.GetString(reader.GetOrdinal("ETA_JUSTIFICATION"));
                        idea.ContactName = reader.IsDBNull(reader.GetOrdinal("CONTACT_PERSON")) ? string.Empty : reader.GetString(reader.GetOrdinal("CONTACT_PERSON"));
                        idea.ContactMobileNo = reader.IsDBNull(reader.GetOrdinal("CONTACT_NUMBER")) ? string.Empty : reader.GetString(reader.GetOrdinal("CONTACT_NUMBER"));
                        idea.ContactEmail = reader.IsDBNull(reader.GetOrdinal("CONTACT_EMAIL")) ? string.Empty : reader.GetString(reader.GetOrdinal("CONTACT_EMAIL"));
                        idea.IdeaStatus = reader.IsDBNull(reader.GetOrdinal("Current_Status")) ? string.Empty : reader.GetString(reader.GetOrdinal("Current_Status"));
                        idea.Id = reader.IsDBNull(reader.GetOrdinal("CREATOR_ID")) ? null : reader.GetGuid(reader.GetOrdinal("CREATOR_ID")).ToString();
                        idea.Name = reader.IsDBNull(reader.GetOrdinal("Creator")) ? string.Empty : reader.GetString(reader.GetOrdinal("Creator"));
                        idea.Email = reader.IsDBNull(reader.GetOrdinal("CREATOR_EMAIL")) ? string.Empty : reader.GetString(reader.GetOrdinal("CREATOR_EMAIL"));
                        idea.ModeratorId = reader.IsDBNull(reader.GetOrdinal("APPROVER_ID")) ? string.Empty : reader.GetGuid(reader.GetOrdinal("APPROVER_ID")).ToString();
                        idea.Moderator = reader.IsDBNull(reader.GetOrdinal("APPROVER")) ? string.Empty : reader.GetString(reader.GetOrdinal("APPROVER"));
                        idea.ModeratorEmail = reader.IsDBNull(reader.GetOrdinal("APPROVER_EMAIL")) ? string.Empty : reader.GetString(reader.GetOrdinal("APPROVER_EMAIL"));
                        idea.DelegatorId = reader.IsDBNull(reader.GetOrdinal("DELEGATE_APPROVER_ID")) ? string.Empty : reader.GetGuid(reader.GetOrdinal("DELEGATE_APPROVER_ID")).ToString();
                        idea.Delegator = reader.IsDBNull(reader.GetOrdinal("DELEGATE_APPROVER")) ? string.Empty : reader.GetString(reader.GetOrdinal("DELEGATE_APPROVER"));
                        idea.DelegatorEmail = reader.IsDBNull(reader.GetOrdinal("DELEGATE_APPROVER_EMAIL")) ? string.Empty : reader.GetString(reader.GetOrdinal("DELEGATE_APPROVER_EMAIL"));
                        idea.PickerId = reader.IsDBNull(reader.GetOrdinal("PICKER_ID")) ? string.Empty : reader.GetGuid(reader.GetOrdinal("PICKER_ID")).ToString();
                        idea.Picker = reader.IsDBNull(reader.GetOrdinal("Picker")) ? string.Empty : reader.GetString(reader.GetOrdinal("Picker"));
                        idea.PickerEmail = reader.IsDBNull(reader.GetOrdinal("PICKER_EMAIL")) ? string.Empty : reader.GetString(reader.GetOrdinal("PICKER_EMAIL"));
                        idea.CreatedDate = reader.IsDBNull(reader.GetOrdinal("Created_Date")) ? null : (DateTimeOffset?)reader.GetDateTimeOffset(reader.GetOrdinal("Created_Date"));
                        idea.UpdatedDate = reader.IsDBNull(reader.GetOrdinal("Updated_Date")) ? null : (DateTimeOffset?)reader.GetDateTimeOffset(reader.GetOrdinal("Updated_Date"));
                        idea.WatchCount = reader.IsDBNull(reader.GetOrdinal("Watch_Count")) ? 0 : (int)reader.GetInt32(reader.GetOrdinal("Watch_Count"));
                        idea.IsWatching = reader.IsDBNull(reader.GetOrdinal("IS_WATCHER")) ? false : reader.GetBoolean(reader.GetOrdinal("IS_WATCHER"));
                        idea.CommentCount = reader.IsDBNull(reader.GetOrdinal("Comment_Count")) ? 0 : (int)reader.GetInt32(reader.GetOrdinal("Comment_Count"));
                        idea.IsSuccess = reader.IsDBNull(reader.GetOrdinal("IS_SUCCESS")) ? false : reader.GetBoolean(reader.GetOrdinal("IS_SUCCESS"));
                        idea.IsSuccess = true;
                        idea.Message = reader.IsDBNull(reader.GetOrdinal("DB_MESSAGE")) ? string.Empty : reader.GetString(reader.GetOrdinal("DB_MESSAGE"));
                        ideas.Add(idea);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return ideas;
        }

        public List<Watcher> GetIdeaWatchers(string email, string author, string ideaId)
        {
            List<Watcher> watchers = new List<Watcher>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_GET_IDEA_WATCHERS]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "List"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", ideaId));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            Watcher watcher = new Watcher()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Watcher_Id")) ? string.Empty : reader.GetGuid(reader.GetOrdinal("Watcher_Id")).ToString(),
                                Name = reader.IsDBNull(reader.GetOrdinal("Watcher_Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Watcher_Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Watcher_Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Watcher_Email")),
                                IdeaId = reader.IsDBNull(reader.GetOrdinal("Idea_Id")) ? string.Empty : reader.GetGuid(reader.GetOrdinal("Idea_Id")).ToString(),
                                IsSuccess = reader.IsDBNull(reader.GetOrdinal("IS_SUCCESS")) ? false : reader.GetBoolean(reader.GetOrdinal("IS_SUCCESS")),
                                Message = reader.IsDBNull(reader.GetOrdinal("DB_MESSAGE")) ? string.Empty : reader.GetString(reader.GetOrdinal("DB_MESSAGE"))
                            };
                            watchers.Add(watcher);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return watchers;
        }

        public List<Comment> GetIdeaComments(string ideaId, string email, string author, int pageSize, int currentPage)
        {
            List<Comment> comments = new List<Comment>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_GET_IDEA_COMMENTS]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "List"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("Idea_Id", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("Page_Size", pageSize));
                sqlCommand.Parameters.Add(new SqlParameter("Current_Page", currentPage));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            Comment comment = new Comment();

                            comment.CommentId = reader.IsDBNull(reader.GetOrdinal("Comment_Id")) ? string.Empty : reader.GetInt64(reader.GetOrdinal("Comment_Id")).ToString();
                            comment.CommentDescription = reader.IsDBNull(reader.GetOrdinal("Comment_Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Comment_Description"));
                            comment.ParentCommentId = reader.IsDBNull(reader.GetOrdinal("Parent_Comment_Id")) ? string.Empty : reader.GetInt64(reader.GetOrdinal("Parent_Comment_Id")).ToString();
                            comment.CreatedDate = reader.IsDBNull(reader.GetOrdinal("Created_Date")) ? null : (DateTimeOffset?)reader.GetDateTimeOffset(reader.GetOrdinal("Created_Date"));
                            comment.UpdatedDate = reader.IsDBNull(reader.GetOrdinal("Updated_Date")) ? null : (DateTimeOffset?)reader.GetDateTimeOffset(reader.GetOrdinal("Updated_Date"));
                            comment.Name = reader.IsDBNull(reader.GetOrdinal("Commenter_Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Commenter_Name"));
                            comment.Email = reader.IsDBNull(reader.GetOrdinal("Commenter_Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Commenter_Email"));
                            comment.Id = reader.IsDBNull(reader.GetOrdinal("Commenter_Id")) ? string.Empty : reader.GetGuid(reader.GetOrdinal("Commenter_Id")).ToString();
                            comment.IdeaId = reader.IsDBNull(reader.GetOrdinal("Idea_Id")) ? string.Empty : reader.GetGuid(reader.GetOrdinal("Idea_Id")).ToString();
                            comment.IsPublic = reader.IsDBNull(reader.GetOrdinal("IS_COMMENT_PUBLIC")) ? false : reader.GetBoolean(reader.GetOrdinal("IS_COMMENT_PUBLIC"));
                            comment.IsSuccess = reader.IsDBNull(reader.GetOrdinal("IS_SUCCESS")) ? false : reader.GetBoolean(reader.GetOrdinal("IS_SUCCESS"));
                            comment.Message = reader.IsDBNull(reader.GetOrdinal("DB_MESSAGE")) ? string.Empty : reader.GetString(reader.GetOrdinal("DB_MESSAGE"));
                            
                            comments.Add(comment);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return comments;
        }

        public List<Alert> GetAlerts(string email, string author, int pageSize, int currentPage)
        {
            List<Alert> alerts = new List<Alert>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_GET_IDEA_ALERTS]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("Page_Size", pageSize));
                sqlCommand.Parameters.Add(new SqlParameter("Current_Page", currentPage));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            Alert alert = new Alert()
                            {
                                AlertId = reader.IsDBNull(reader.GetOrdinal("Alert_Id")) ? string.Empty : reader.GetGuid(reader.GetOrdinal("Alert_Id")).ToString(),
                                Id = reader.IsDBNull(reader.GetOrdinal("Alert_User_Id")) ? string.Empty : reader.GetGuid(reader.GetOrdinal("Alert_User_Id")).ToString(),
                                IdeaId = reader.IsDBNull(reader.GetOrdinal("Alert_Idea_Id")) ? string.Empty : reader.GetGuid(reader.GetOrdinal("Alert_Idea_Id")).ToString(),
                                IdeaName = reader.IsDBNull(reader.GetOrdinal("Alert_Idea_Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Alert_Idea_Name")),
                                AlertDescription = reader.IsDBNull(reader.GetOrdinal("Alert_Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Alert_Description")),
                                CreatedDate = reader.IsDBNull(reader.GetOrdinal("Created_Date")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("Created_Date")),
                                AlertType = reader.IsDBNull(reader.GetOrdinal("Alert_Type")) ? string.Empty : reader.GetString(reader.GetOrdinal("Alert_Type")),
                                AlertFlag = reader.IsDBNull(reader.GetOrdinal("Alert_Flag")) ? false : reader.GetBoolean(reader.GetOrdinal("Alert_Flag")),
                                IsSuccess = reader.IsDBNull(reader.GetOrdinal("IS_SUCCESS")) ? false : reader.GetBoolean(reader.GetOrdinal("IS_SUCCESS")),
                                Message = reader.IsDBNull(reader.GetOrdinal("DB_MESSAGE")) ? string.Empty : reader.GetString(reader.GetOrdinal("DB_MESSAGE"))
                            };
                            alerts.Add(alert);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return alerts;
        }

        public Idea GetIdeaDetails(string email, string author, string ideaId)
        {
            Idea idea = new Idea();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QDEASP_GET_IDEA_DETAILS]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", author));
                sqlCommand.Parameters.Add(new SqlParameter("QDEA_USER_ID", email));
                sqlCommand.Parameters.Add(new SqlParameter("Idea_Id", ideaId));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            //Set Idea

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
            return idea;
        }

        private InviteeList GetInviteeList(List<User> users)
        {
            InviteeList inviteeList = new InviteeList();
            if (users.Any())
            {
                inviteeList.IsSuccess = users.First().IsSuccess;
                inviteeList.Message = users.First().Message;
            }
            List<User> listWatchers = new List<User>();
            List<User> listApprover = new List<User>();
            List<User> listCommenter = new List<User>();

            foreach (User userItem in users)
            {
                if (userItem.Role.Contains("Approver"))
                {
                    listApprover.Add(userItem);
                }
                else if (userItem.Role.Contains("Creator"))
                {
                    inviteeList.IdeaCreator = userItem;
                }
                else if (userItem.Role.Contains("Picker"))
                {
                    inviteeList.IdeaPicker = userItem;
                }
                else if (userItem.Role.Contains("Watcher"))
                {
                    listWatchers.Add(userItem);
                }
                else if (userItem.Role.Contains("Commenter"))
                {
                    listCommenter.Add(userItem);
                }
            }
            inviteeList.IdeaWatchers = listWatchers;
            inviteeList.IdeaModerators = listApprover;
            inviteeList.IdeaCommenters = listCommenter;
            return inviteeList;
        }

        private User ReturnUser(SqlDataReader reader)
        {
            User user = new User();
            while (reader.Read())
            {
                user.Id = reader.GetGuid(reader.GetOrdinal("AUTHORIZATION_ID")).ToString();
                user.Name = reader.IsDBNull(reader.GetOrdinal("QDEA_USER_NAME")) ? string.Empty : reader.GetString(reader.GetOrdinal("QDEA_USER_NAME"));
                user.Email = reader.IsDBNull(reader.GetOrdinal("QDEA_USER_ID")) ? string.Empty : reader.GetString(reader.GetOrdinal("QDEA_USER_ID"));
                user.Role = reader.IsDBNull(reader.GetOrdinal("QDEA_ROLE")) ? string.Empty : reader.GetString(reader.GetOrdinal("QDEA_ROLE"));
                user.IsSuccess = reader.IsDBNull(reader.GetOrdinal("IS_SUCCESS")) ? false : reader.GetBoolean(reader.GetOrdinal("IS_SUCCESS"));
                user.Message = reader.IsDBNull(reader.GetOrdinal("DB_MESSAGE")) ? string.Empty : reader.GetString(reader.GetOrdinal("DB_MESSAGE"));
            }
            return user;
        }

        private List<User> ReturnUserList(SqlDataReader reader)
        {
            List<User> userList = new List<User>();
            while (reader.Read())
            {
                User user = new User();
                user.Id = reader.GetGuid(reader.GetOrdinal("AUTHORIZATION_ID")).ToString();
                user.Name = reader.IsDBNull(reader.GetOrdinal("QDEA_USER_NAME")) ? string.Empty : reader.GetString(reader.GetOrdinal("QDEA_USER_NAME"));
                user.Email = reader.IsDBNull(reader.GetOrdinal("QDEA_USER_ID")) ? string.Empty : reader.GetString(reader.GetOrdinal("QDEA_USER_ID"));
                user.Role = reader.IsDBNull(reader.GetOrdinal("QDEA_ROLE")) ? string.Empty : reader.GetString(reader.GetOrdinal("QDEA_ROLE"));
                user.IsSuccess = reader.IsDBNull(reader.GetOrdinal("IS_SUCCESS")) ? false : reader.GetBoolean(reader.GetOrdinal("IS_SUCCESS"));
                user.Message = reader.IsDBNull(reader.GetOrdinal("DB_MESSAGE")) ? string.Empty : reader.GetString(reader.GetOrdinal("DB_MESSAGE"));
                userList.Add(user);
            }
            return userList;
        }
    }
}