using AutoMapper;
using Opinio.API.Models.Review;
using Opinio.Core.Entities;
using Opinio.Core.Helpers;

namespace Opinio.API.Mappers;

public class ReviewMappingProfile : Profile
{
    public ReviewMappingProfile()
    {
        #region Create
        CreateMap<ReviewRequest, Review>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images ?? new List<ReviewImageRequest>()));

        CreateMap<ReviewImageRequest, ReviewImage>();

        CreateMap<Review, ReviewResponse>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

        CreateMap<ReviewImage, ReviewImageResponse>();

        CreateMap<OperationResult<Review>, OperationResult<ReviewResponse>>()
            .ConstructUsing(
                (src, ctx) =>
                    new OperationResult<ReviewResponse>(
                        data: ctx.Mapper.Map<ReviewResponse>(src.Data),
                        validationErrors: src.ValidationErrors,
                        message: src.Message,
                        httpCode: src.HttpCode,
                        status: src.Status
                    )
            );
        #endregion
        #region GetById
        CreateMap<Review, GetReviewResponse>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

        CreateMap<OperationResult<Review>, OperationResult<GetReviewResponse>>()
            .ConstructUsing(
                (src, ctx) =>
                    new OperationResult<GetReviewResponse>(
                        data: ctx.Mapper.Map<GetReviewResponse>(src.Data),
                        validationErrors: src.ValidationErrors,
                        message: src.Message,
                        httpCode: src.HttpCode,
                        status: src.Status
                    )
            );
        #endregion
        #region List

        CreateMap<PaginatedResult<Review>, PaginatedResult<GetReviewResponse>>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<OperationResult<PaginatedResult<Review>>, OperationResult<PaginatedResult<GetReviewResponse>>>()
            .ConstructUsing(
                (src, ctx) =>
                    new OperationResult<PaginatedResult<GetReviewResponse>>(
                        data: ctx.Mapper.Map<PaginatedResult<GetReviewResponse>>(src.Data),
                        validationErrors: src.ValidationErrors,
                        message: src.Message,
                        httpCode: src.HttpCode,
                        status: src.Status
                    )
            );
        #endregion
    }
}
