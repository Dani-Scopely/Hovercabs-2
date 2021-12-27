using System;
using Hovercabs.Models.DTO;

namespace Hovercabs.Models
{
    [Serializable]
    public class Profile
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public Profile(ProfileDto profileDto)
        {
            Username = profileDto.Username;
            Password = profileDto.Password;
        }
    }
}