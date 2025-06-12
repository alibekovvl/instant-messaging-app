using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstantMessagingApp.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SendMessageController
{
  
}