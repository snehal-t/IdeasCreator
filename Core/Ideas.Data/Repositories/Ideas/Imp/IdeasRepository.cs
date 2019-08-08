using System;
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

        public InviteeList ApproveIdea(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public InviteeList CreateIdea(Idea idea)
        {
            List<User> usersForNotification = new List<User>();
            
            _dbConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("QUARC.spCreateIdea", (SqlConnection)_dbConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("Email", idea.Name));
            sqlCommand.Parameters.Add(new SqlParameter("Type", idea.Type));
            sqlCommand.Parameters.Add(new SqlParameter("Title", idea.Title));
            sqlCommand.Parameters.Add(new SqlParameter("Source", idea.Source));
            sqlCommand.Parameters.Add(new SqlParameter("Description", idea.Description));
            sqlCommand.Parameters.Add(new SqlParameter("BusinessCase", idea.BusinessCase));
            sqlCommand.Parameters.Add(new SqlParameter("IdealTime", idea.IdealTime));
            sqlCommand.Parameters.Add(new SqlParameter("BusinessJustification", idea.BusinessJustification));
            sqlCommand.Parameters.Add(new SqlParameter("ContactName", idea.ContactName));
            sqlCommand.Parameters.Add(new SqlParameter("ContactEmail", idea.ContactEmail));
            sqlCommand.Parameters.Add(new SqlParameter("ContactMobileNo", idea.ContactMobileNo));

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
            return GetInviteeList(usersForNotification);
        }

        public InviteeList DeligateIdea(string ideaId, User assignee, string userComments)
        {
            throw new NotImplementedException();
        }

        public List<Alert> GetAlerts(string email)
        {
            throw new NotImplementedException();
        }

        public List<Idea> GetIdeas(string email, string IdeaStatus)
        {
            throw new NotImplementedException();
        }

        public List<Comment> IdeaComments(string ideaId, string email)
        {
            throw new NotImplementedException();
        }

        public InviteeList PickIdea(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public InviteeList PickIdeaAccept(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public InviteeList PickIdeaDone(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public InviteeList PickIdeaGiveUp(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public InviteeList PickIdeaRework(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public InviteeList RejectIdea(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        public InviteeList UpdateIdea(Idea idea)
        {
            throw new NotImplementedException();
        }

        public InviteeList WatchIdea(string ideaId, string email)
        {
            throw new NotImplementedException();
        }

        public InviteeList WithDrawIdea(string ideaId, string email, string userComments)
        {
            throw new NotImplementedException();
        }

        private InviteeList GetInviteeList(List<User> users)
        {
            InviteeList inviteeList = new InviteeList();
            List<User> listWatchers = new List<User>();
            List<User> listApprover = new List<User>();
            List<User> listCommenter = new List<User>();

            foreach (User userItem in users)
            {
                switch(userItem.Role)
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
