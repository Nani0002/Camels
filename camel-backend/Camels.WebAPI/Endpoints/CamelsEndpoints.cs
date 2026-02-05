using AutoMapper;
using Camels.DataAccess.Exceptions;
using Camels.DataAccess.Models;
using Camels.DataAccess.Services;
using Camels.Shared.Models;

namespace Camels.WebAPI.Endpoints
{
    public static class CamelsEndpoints
    {

        public static void MapCamelsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/camels")
                           .WithTags("Camels");

            // GET ALL
            group.MapGet("/", async (ICamelsService camelsService, IMapper mapper) =>
            {
                var camels = await camelsService.GetAllAsync();
                var dtos = mapper.Map<List<CamelResponseDto>>(camels);

                return Results.Ok(dtos);
            })
            .WithSummary("Get all camels")
            .Produces<List<CamelResponseDto>>(StatusCodes.Status200OK);

            // GET BY ID
            group.MapGet("/{id:int}", async (int id, ICamelsService camelsService, IMapper mapper) =>
            {
                try
                {
                    var camel = await camelsService.GetByIdAsync(id);
                    var dto = mapper.Map<CamelResponseDto>(camel);

                    return Results.Ok(dto);
                }
                catch (EntityNotFoundException)
                {
                    return Results.NotFound();
                }
            })
            .WithSummary("Get camel by id")
            .Produces<CamelResponseDto>(200)
            .Produces(404);

            // CREATE
            group.MapPost("/", async (CamelRequestDto requestDto, ICamelsService camelsService, IMapper mapper) =>
            {
                try
                {
                    var camel = mapper.Map<Camel>(requestDto);

                    await camelsService.AddAsync(camel);

                    var responseDto = mapper.Map<CamelResponseDto>(camel);

                    return Results.Created(
                        $"/camels/{responseDto.Id}",
                        responseDto);
                }
                catch (InvalidDataException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (SaveFailedException)
                {
                    return Results.Conflict();
                }
            })
            .WithSummary("Create camel")
            .Produces<CamelResponseDto>(201)
            .Produces(400)
            .Produces(409);

            // UPDATE
            group.MapPut("/{id:int}", async (int id, CamelRequestDto requestDto, ICamelsService camelsService, IMapper mapper) =>
            {
                try
                {
                    var camel = mapper.Map<Camel>(requestDto);
                    camel.Id = id;

                    await camelsService.UpdateAsync(camel);

                    var responseDto = mapper.Map<CamelResponseDto>(camel);

                    return Results.Ok(responseDto);
                }
                catch (EntityNotFoundException)
                {
                    return Results.NotFound();
                }
                catch (InvalidDataException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (SaveFailedException)
                {
                    return Results.Conflict();
                }
            })
            .WithSummary("Update camel")
            .Produces<CamelResponseDto>(200)
            .Produces(400)
            .Produces(404)
            .Produces(409);

            // DELETE
            group.MapDelete("/{id:int}", async (int id, ICamelsService camelsService) =>
            {
                try
                {
                    await camelsService.DeleteAsync(id);
                    return Results.NoContent();
                }
                catch (EntityNotFoundException)
                {
                    return Results.NotFound();
                }
                catch (SaveFailedException)
                {
                    return Results.Conflict();
                }
            })
            .WithSummary("Delete camel")
            .Produces(204)
            .Produces(404)
            .Produces(409);
        }
    }
}