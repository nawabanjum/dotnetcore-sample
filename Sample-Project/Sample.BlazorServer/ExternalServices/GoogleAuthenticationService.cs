//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Http;

//public partial class GoogleLogin : ComponentBase
//{
//    [Inject] private NavigationManager NavigationManager { get; set; }
//    [Inject] private IHttpContextAccessor HttpContextAccessor { get; set; }

//    private async Task LoginWithGoogle()
//    {
//        var authenticationProperties = new AuthenticationProperties
//        {
//            RedirectUri = NavigationManager.Uri // Redirect back to the current page after login
//        };

//        await HttpContextAccessor.HttpContext.ChallengeAsync("Google", authenticationProperties);
//    }
//}
