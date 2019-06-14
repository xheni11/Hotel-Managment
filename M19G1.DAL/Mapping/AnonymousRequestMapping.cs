using M19G1.DAL.Entities;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Mapping
{
    public class AnonymousRequestMapping
    {
               
        public static AnonymousRequestModel ToModel(AnonymousRequest user)
        {
            
            return new AnonymousRequestModel
            {
                Id = user.Id,
                Confirmed=user.Confirmed,
                Username=user.User.UserName,
                FirstName=user.User.FirstName,
                LastName=user.User.LastName,
                Email=user.User.Email,
                Gender=user.User.Gender
                
            };
        }
        public static AnonymousRequest ToEntityToCreate(int idUser)
        {
            return new AnonymousRequest
            {
                Confirmed = false,
                UserId=idUser
            };
        }
        public static List<AnonymousRequestModel> ToModel(IEnumerable<AnonymousRequest> user)
        {
            return user.Select(ToModel).ToList();
        }
    }
}
