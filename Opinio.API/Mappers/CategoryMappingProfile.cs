using AutoMapper;
using Opinio.API.Models.Category;
using Opinio.Core.Entities;
using Opinio.Core.Helpers;

namespace Opinio.API.Mappers;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        #region Create
        CreateMap<CreateCategoryRequest, Category>();

        CreateMap<Category, CreateCategoryResponse>();

        CreateMap<OperationResult<Category>, OperationResult<CreateCategoryResponse>>()
            .ConstructUsing(
                (src, ctx) =>
                    new OperationResult<CreateCategoryResponse>(
                        data: ctx.Mapper.Map<CreateCategoryResponse>(src.Data),
                        validationErrors: src.ValidationErrors,
                        message: src.Message,
                        httpCode: src.HttpCode,
                        status: src.Status
                    )
            );
        #endregion

        #region Update
        CreateMap<UpdateCategoryRequest, Category>();

        CreateMap<Category, UpdateCategoryResponse>();

        CreateMap<OperationResult<Category>, OperationResult<UpdateCategoryResponse>>()
            .ConstructUsing(
                (src, ctx) =>
                    new OperationResult<UpdateCategoryResponse>(
                        data: ctx.Mapper.Map<UpdateCategoryResponse>(src.Data),
                        validationErrors: src.ValidationErrors,
                        message: src.Message,
                        httpCode: src.HttpCode,
                        status: src.Status
                    )
            );
        #endregion

        #region GetById
        CreateMap<Category, GetCategoryResponse>();

        CreateMap<OperationResult<Category>, OperationResult<GetCategoryResponse>>()
            .ConstructUsing(
                (src, ctx) =>
                    new OperationResult<GetCategoryResponse>(
                        data: ctx.Mapper.Map<GetCategoryResponse>(src.Data),
                        validationErrors: src.ValidationErrors,
                        message: src.Message,
                        httpCode: src.HttpCode,
                        status: src.Status
                    )
            );
        #endregion
    }
}
