using System;
using Hovercabs.Models.DTO;
using Newtonsoft.Json;

namespace Hovercabs.Models
{
    [Serializable]
    public class Profile
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public int Xenits { get; set; }
        public int Level { get; set; }

        public Profile(string raw)
        {
            var profileDto = JsonConvert.DeserializeObject<ProfileDto>(raw);
            Build(profileDto);
        }
        
        private void Build(ProfileDto profileDto)
        {
            Username = profileDto.Username;
            Password = profileDto.Password;
            Xenits = profileDto.Xenits;
            Level = profileDto.Level;
        }

        public ProfileDto ToDto()
        {
            var pDto = new ProfileDto();
            pDto.Username = Username;
            pDto.Password = Password;
            pDto.Level = Level;
            pDto.Xenits = Xenits;
            return pDto;
        }
    }
}