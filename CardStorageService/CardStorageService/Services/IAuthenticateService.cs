using CardStorageService.Models;
using CardStorageService.Models.Requests;

namespace CardStorageService.Services
{
    public interface IAuthenticateService
    {
        AuthenticationResponse Login(AuthenticationRequest authenticationRequest);
        SessionInfo GetSessionInfo(string sessionToken);
    }
}
