﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ideas.Models;
using System.Data.SqlClient;

namespace Ideas.Data.Repositories.Ideas.Imp
{
    public class IdeasRepository : RepositoryBase, IIdeasRepository
    {
        private readonly IDbConnection _dbConnection;
        public IdeasRepository(IDbConnection dbConnection) => _dbConnection = dbConnection;

        public InviteeList CreateIdea(Idea idea, string email)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Add"));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_ID", null));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_TYPE", idea.Type));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_NAME", idea.Name));
                sqlCommand.Parameters.Add(new SqlParameter("TITLE", idea.Title));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_SOURCE", idea.Source));
                sqlCommand.Parameters.Add(new SqlParameter("IDEA_DESCRIPTION", idea.Description));
                sqlCommand.Parameters.Add(new SqlParameter("BUSINESS_CASE", idea.BusinessCase));
                sqlCommand.Parameters.Add(new SqlParameter("ETA", idea.IdealTime));
                sqlCommand.Parameters.Add(new SqlParameter("ETA_JUSTIFICATION", idea.BusinessJustification));
                sqlCommand.Parameters.Add(new SqlParameter("CONTACT_PERSON", idea.ContactName));
                sqlCommand.Parameters.Add(new SqlParameter("CONTACT_NUMBER", idea.ContactEmail));
                sqlCommand.Parameters.Add(new SqlParameter("CONTACT_EMAIL", idea.ContactMobileNo));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            User user = new User()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? string.Empty : reader.GetString(reader.GetOrdinal("Role"))
                            };
                            usersForNotification.Add(user);
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
            return GetInviteeList(usersForNotification);
        }

        public InviteeList UpdateIdea(Idea idea, string email)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
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
                sqlCommand.Parameters.Add(new SqlParameter("CONTACT_NUMBER", idea.ContactEmail));
                sqlCommand.Parameters.Add(new SqlParameter("CONTACT_EMAIL", idea.ContactMobileNo));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            User user = new User()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? string.Empty : reader.GetString(reader.GetOrdinal("Role"))
                            };
                            usersForNotification.Add(user);
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
            return GetInviteeList(usersForNotification);
        }

        public InviteeList WithDrawIdea(string ideaId, string email, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Withdraw"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("IdeaId", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));
                
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            User user = new User()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? string.Empty : reader.GetString(reader.GetOrdinal("Role"))
                            };
                            usersForNotification.Add(user);
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
            return GetInviteeList(usersForNotification);
        }

        public InviteeList ApproveIdea(string ideaId, string email, string userComments, bool commentType, long commentParentId)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Approve"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("IdeaId", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_PARENT_COMMENT_ID", commentParentId));
                sqlCommand.Parameters.Add(new SqlParameter("IS_COMMENT_PUBLIC", commentType));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            User user = new User()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? string.Empty : reader.GetString(reader.GetOrdinal("Role"))
                            };
                            usersForNotification.Add(user);
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
            return GetInviteeList(usersForNotification);
        }

        public InviteeList RejectIdea(string ideaId, string email, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Reject"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("IdeaId", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));
                
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            User user = new User()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? string.Empty : reader.GetString(reader.GetOrdinal("Role"))
                            };
                            usersForNotification.Add(user);
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
            return GetInviteeList(usersForNotification);
        }

        public InviteeList DeligateIdea(string ideaId, User assignee, string userComments, string email, bool commentType, long commentParentId)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Delegate"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("IdeaId", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("DELECATE_APPROVER_EMAIL", assignee.Email));
                sqlCommand.Parameters.Add(new SqlParameter("DELECATE_APPROVER_NAME", assignee.Name));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_PARENT_COMMENT_ID", commentParentId));
                sqlCommand.Parameters.Add(new SqlParameter("IS_COMMENT_PUBLIC", commentType));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            User user = new User()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? string.Empty : reader.GetString(reader.GetOrdinal("Role"))
                            };
                            usersForNotification.Add(user);
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
            return GetInviteeList(usersForNotification);
        }

        public InviteeList PickIdea(string ideaId, string email, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Pick"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("IdeaId", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));
                
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            User user = new User()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? string.Empty : reader.GetString(reader.GetOrdinal("Role"))
                            };
                            usersForNotification.Add(user);
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
            return GetInviteeList(usersForNotification);
        }

        public InviteeList PickIdeaDone(string ideaId, string email, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Done"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("IdeaId", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            User user = new User()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? string.Empty : reader.GetString(reader.GetOrdinal("Role"))
                            };
                            usersForNotification.Add(user);
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
            return GetInviteeList(usersForNotification);
        }

        public InviteeList PickIdeaGiveUp(string ideaId, string email, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "GiveUp"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("IdeaId", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            User user = new User()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? string.Empty : reader.GetString(reader.GetOrdinal("Role"))
                            };
                            usersForNotification.Add(user);
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
            return GetInviteeList(usersForNotification);
        }

        public InviteeList PickIdeaRework(string ideaId, string email, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Rework"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("IdeaId", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            User user = new User()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? string.Empty : reader.GetString(reader.GetOrdinal("Role"))
                            };
                            usersForNotification.Add(user);
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
            return GetInviteeList(usersForNotification);
        }

        public InviteeList PickIdeaAccept(string ideaId, string email, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Accept"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("IdeaId", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            User user = new User()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? string.Empty : reader.GetString(reader.GetOrdinal("Role"))
                            };
                            usersForNotification.Add(user);
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
            return GetInviteeList(usersForNotification);
        }

        public InviteeList PickIdeaReopen(string ideaId, string email, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Reopen"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("IdeaId", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            User user = new User()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? string.Empty : reader.GetString(reader.GetOrdinal("Role"))
                            };
                            usersForNotification.Add(user);
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
            return GetInviteeList(usersForNotification);
        }

        public bool WatchIdea(string ideaId, string email, bool isActive)
        {
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Watch"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("IdeaId", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("Watch", isActive));
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
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

        public InviteeList CommentIdea(string ideaId, string email, bool commentType, long commentParentId, string userComments)
        {
            List<User> usersForNotification = new List<User>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_IDEA_MANAGEMENT]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("MANAGMENT_TYPE", "Comment"));
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("IdeaId", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_DESCRIPTION", userComments));
                sqlCommand.Parameters.Add(new SqlParameter("COMMENT_PARENT_COMMENT_ID", commentParentId));
                sqlCommand.Parameters.Add(new SqlParameter("IS_COMMENT_PUBLIC", commentType));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            User user = new User()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? string.Empty : reader.GetString(reader.GetOrdinal("Role"))
                            };
                            usersForNotification.Add(user);
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
            return GetInviteeList(usersForNotification);
        }

        public List<Idea> GetIdeas(string email, string ideaPage, int pageSize, int currentPage, string orderBy, string order)
        {
            List<Idea> ideas = new List<Idea>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_GET_IDEA_LIST]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("IdeaPage", ideaPage));
                sqlCommand.Parameters.Add(new SqlParameter("Page_Size", pageSize));
                sqlCommand.Parameters.Add(new SqlParameter("Current_Page", currentPage));
                sqlCommand.Parameters.Add(new SqlParameter("Order_By", orderBy));
                sqlCommand.Parameters.Add(new SqlParameter("Sort_Order", order));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            Idea idea = new Idea()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Idea_Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Idea_Id")),
                                Type = reader.IsDBNull(reader.GetOrdinal("Idea_Type")) ? string.Empty : reader.GetString(reader.GetOrdinal("Idea_Type")),
                                IdeaName = reader.IsDBNull(reader.GetOrdinal("Idea_Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Idea_Name")),
                                Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? string.Empty : reader.GetString(reader.GetOrdinal("Title")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Creator")) ? string.Empty : reader.GetString(reader.GetOrdinal("Creator")),
                                Moderator = reader.IsDBNull(reader.GetOrdinal("Approver")) ? string.Empty : reader.GetString(reader.GetOrdinal("Approver")),
                                Picker = reader.IsDBNull(reader.GetOrdinal("Picker")) ? string.Empty : reader.GetString(reader.GetOrdinal("Picker")),
                                CreatedDate = reader.IsDBNull(reader.GetOrdinal("Created_Date")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("Created_Date")),
                                UpdatedDate = reader.IsDBNull(reader.GetOrdinal("Updated_Date")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("Updated_Date")),
                                WatchCount = reader.IsDBNull(reader.GetOrdinal("Watch_Count")) ? 0 : (int)reader.GetInt32(reader.GetOrdinal("Watch_Count")),
                                CommentCount = reader.IsDBNull(reader.GetOrdinal("Comment_Count")) ? 0 : (int)reader.GetInt32(reader.GetOrdinal("Comment_Count")),
                                IdeaStatus = reader.IsDBNull(reader.GetOrdinal("Current_Status")) ? string.Empty : reader.GetString(reader.GetOrdinal("Current_Status"))
                            };
                            ideas.Add(idea);
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
            return ideas;
        }

        public List<Watcher> GetIdeaWatchers(string email, string ideaId)
        {
            List<Watcher> watchers = new List<Watcher>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_GET_IDEA_WATCHERS]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("IdeaId", ideaId));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            Watcher watcher = new Watcher()
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Watcher_Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Watcher_Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Watcher_Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Watcher_Name")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Watcher_Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Watcher_Email")),
                                IdeaId = reader.IsDBNull(reader.GetOrdinal("Idea_Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Idea_Id"))
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

        public List<Comment> GetIdeaComments(string ideaId, string email, int pageSize, int currentPage)
        {
            List<Comment> comments = new List<Comment>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_GET_IDEA_COMMENTS]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                sqlCommand.Parameters.Add(new SqlParameter("Idea_Id", ideaId));
                sqlCommand.Parameters.Add(new SqlParameter("Page_Size", pageSize));
                sqlCommand.Parameters.Add(new SqlParameter("Current_Page", currentPage));

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            Comment comment = new Comment()
                            {
                                CommentId = reader.IsDBNull(reader.GetOrdinal("Comment_Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Comment_Id")),
                                CommentDescription = reader.IsDBNull(reader.GetOrdinal("Comment_Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Comment_Description")),
                                ParentCommentId = reader.IsDBNull(reader.GetOrdinal("Parent_Comment_Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Parent_Comment_Id")),
                                CreatedDate = reader.IsDBNull(reader.GetOrdinal("Created_Date")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("Created_Date")),
                                UpdatedDate = reader.IsDBNull(reader.GetOrdinal("Updated_Date")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("Updated_Date")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Created_By")) ? string.Empty : reader.GetString(reader.GetOrdinal("Created_By")),
                                IdeaId = reader.IsDBNull(reader.GetOrdinal("Idea_Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Idea_Id"))
                            };
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

        public List<Alert> GetAlerts(string email)
        {
            List<Alert> alerts = new List<Alert>();
            try
            {
                _dbConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[QDEA].[QEADSP_GET_IDEA_ALERTS]", (SqlConnection)_dbConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("CREATED_BY", email));
                
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            Alert alert = new Alert()
                            {
                                AlertId = reader.IsDBNull(reader.GetOrdinal("Alert_Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Alert_Id")),
                                Id = reader.IsDBNull(reader.GetOrdinal("Alert_User_Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Alert_User_Id")),
                                IdeaId = reader.IsDBNull(reader.GetOrdinal("Alert_Idea_Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("Alert_Idea_Id")),
                                IdeaName = reader.IsDBNull(reader.GetOrdinal("Alert_Idea_Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Alert_Idea_Name")),
                                AlertDescription = reader.IsDBNull(reader.GetOrdinal("Alert_Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Alert_Description")),
                                CreatedDate = reader.IsDBNull(reader.GetOrdinal("Created_Date")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("Created_Date")),
                                AlertType = reader.IsDBNull(reader.GetOrdinal("Alert_Type")) ? string.Empty : reader.GetString(reader.GetOrdinal("Alert_Type")),
                                AlertFlag = reader.IsDBNull(reader.GetOrdinal("Alert_Flag")) ? false : reader.GetBoolean(reader.GetOrdinal("Alert_Flag")),
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

        private InviteeList GetInviteeList(List<User> users)
        {
            InviteeList inviteeList = new InviteeList();
            List<User> listWatchers = new List<User>();
            List<User> listApprover = new List<User>();
            List<User> listCommenter = new List<User>();

            foreach (User userItem in users)
            {
                switch (userItem.Role)
                {
                    case "Approver":
                        listApprover.Add(userItem);
                        break;
                    case "Creator":
                        inviteeList.IdeaCreator = userItem;
                        break;
                    case "Picker":
                        inviteeList.IdeaPicker = userItem;
                        break;
                    case "Watcher":
                        listWatchers.Add(userItem);
                        break;
                    case "Commenter":
                        listCommenter.Add(userItem);
                        break;
                }
            }
            inviteeList.IdeaWatchers = listWatchers;
            inviteeList.IdeaModerators = listApprover;
            inviteeList.IdeaCommenters = listCommenter;
            return inviteeList;
        }
    }
}