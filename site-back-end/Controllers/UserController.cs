using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LudoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase 
{
    public LudoContext _ludocontext;

    public UserController(LudoContext ludoContext)
    {
        _ludocontext = ludoContext;
    }

    [HttpGet("teste")]
    public string Get()
    {
        return "oi";
    }

    [HttpPost("subscribe")]
    public IResult SubscribeUser([FromBody] SubscribeUserBody jsonBody)
    {

        //checando se já existe um usuário com username ou email
        var UsuariosParecidos = _ludocontext.Users.Where(u => u.username == jsonBody.Username || u.email == jsonBody.Email).ToList();

        //se existir retornar um Conflict 
        if(UsuariosParecidos.Count() > 0)
        {
            return Results.Conflict("Email ou Username já estão sendo usados!");
        }
        //se não existir realizar o cadastro do usuário
        else
        {
            var userid = Guid.NewGuid().ToString();
            User user = new User(){
                id = userid,
                username = jsonBody.Username,
                email = jsonBody.Email,
                password = jsonBody.Password,
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };
            _ludocontext.Users.Add(user);
            
            UserCosmetics userCosmetics = new UserCosmetics(){
                user_id = userid,
                available_cosmetics = [],
                wishlist_cosmetics = []
            };
            _ludocontext.UserCosmetics.Add(userCosmetics);

            _ludocontext.SaveChanges();
        }
        return Results.Created();
    }

    [HttpPost("login")]
    public IResult LoginUser ([FromBody] LoginUserBody jsonBody)
    {
        //procura se existe um usuário com tal senha e nome no banco
        var UsuarioEncontrado = _ludocontext.Users.Where(u => u.username == jsonBody.Username && u.password == jsonBody.Password).FirstOrDefault();
        if(UsuarioEncontrado == null)
        {
            return Results.Unauthorized();
        }
        var userToken = JwtService.GenerateJwtToken(UsuarioEncontrado.id);
        return Results.Ok(userToken);
    }
}