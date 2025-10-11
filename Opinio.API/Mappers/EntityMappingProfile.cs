using AutoMapper;
using Opinio.API.Models.Entity;
using Opinio.Core.Entities;
using Opinio.Core.Helpers;

namespace Opinio.API.Mappers;

public class EntityMappingProfile : Profile
{
    public EntityMappingProfile()
    {
        #region Create
        CreateMap<CreateEntityRequest, Entity>();

        CreateMap<Entity, CreateEntityResponse>();

        CreateMap<OperationResult<Entity>, OperationResult<CreateEntityResponse>>()
            .ConstructUsing(
                (src, ctx) =>
                    new OperationResult<CreateEntityResponse>(
                        data: ctx.Mapper.Map<CreateEntityResponse>(src.Data),
                        validationErrors: src.ValidationErrors,
                        message: src.Message,
                        httpCode: src.HttpCode,
                        status: src.Status
                    )
            );
        #endregion

        #region Update
        CreateMap<UpdateEntityRequest, Entity>();

        CreateMap<Entity, UpdateEntityResponse>();

        CreateMap<OperationResult<Entity>, OperationResult<UpdateEntityResponse>>()
            .ConstructUsing(
                (src, ctx) =>
                    new OperationResult<UpdateEntityResponse>(
                        data: ctx.Mapper.Map<UpdateEntityResponse>(src.Data),
                        validationErrors: src.ValidationErrors,
                        message: src.Message,
                        httpCode: src.HttpCode,
                        status: src.Status
                    )
            );
        #endregion

        #region GetById
        CreateMap<Entity, GetEntityResponse>()
        .ForMember(dest => dest.CategoryName,
            opt => opt.MapFrom(src => src.Category == null ? null : src.Category.Name));

        CreateMap<OperationResult<Entity>, OperationResult<GetEntityResponse>>()
            .ConstructUsing(
                (src, ctx) =>
                    new OperationResult<GetEntityResponse>(
                        data: ctx.Mapper.Map<GetEntityResponse>(src.Data),
                        validationErrors: src.ValidationErrors,
                        message: src.Message,
                        httpCode: src.HttpCode,
                        status: src.Status
                    )
            );
        #endregion

        #region List
        CreateMap<Entity, GetEntityResponse>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category == null ? null : src.Category.Name));

        CreateMap<OperationResult<List<Entity>>, OperationResult<List<GetEntityResponse>>>()
            .ConstructUsing(
                (src, ctx) =>
                    new OperationResult<List<GetEntityResponse>>(
                        data: ctx.Mapper.Map<List<GetEntityResponse>>(src.Data),
                        validationErrors: src.ValidationErrors,
                        message: src.Message,
                        httpCode: src.HttpCode,
                        status: src.Status
                    )
            );
        #endregion

    }
}
