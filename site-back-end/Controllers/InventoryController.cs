using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class InventoryController: ControllerBase
{
    public LudoContext _ludocontext;
    public IMapper _mapper;

    public InventoryController(LudoContext ludoContext, IMapper mapper) 
    {
        _ludocontext = ludoContext;
        _mapper = mapper;
    }

    [HttpGet("list")]
    public IResult ListInventory ([FromBody] ListInventoryBody jsonBody)
    {
        // primeiro pego a lista de ids de cosmeticos que o usuario tem
        UserCosmetics cosmeticosEncontrados = _ludocontext.UserCosmetics.Where(x => x.user_id == jsonBody.UserId).FirstOrDefault();

        if (cosmeticosEncontrados == null)
        {
            return Results.Problem("inventario não foi encontrado");
        }

        List<string> listaDeIds = cosmeticosEncontrados.available_cosmetics;

        // depois com esses ids, capturo todos os cosmeticos da lista de cosmeticos
        List<Cosmetic> ListaGerada = _ludocontext.Cosmetics
            .Where(e => listaDeIds.Contains(e.id))
            .Skip(jsonBody.ItemsPerPage * jsonBody.Page)
            .Take(jsonBody.ItemsPerPage)
            .ToList();

        // filtra o resultado para voltar informações especificas do cosmético
        List<ListCosmeticsResponse> ListaFiltrada = _mapper.Map<List<ListCosmeticsResponse>>(ListaGerada); 
        return Results.Ok(ListaFiltrada);
    }
}