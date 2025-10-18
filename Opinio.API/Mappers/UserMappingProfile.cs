using AutoMapper;
using Opinio.API.Models.User;
using Opinio.Core.Entities;
using Opinio.Core.Helpers;

namespace Opinio.API.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        #region Register
        CreateMap<CreateUserRequest, User>();

        CreateMap<User, CreateUserResponse>();

        CreateMap<OperationResult<User>, OperationResult<CreateUserResponse>>()
            .ConstructUsing(
                (src, ctx) =>
                    new OperationResult<CreateUserResponse>(
                        data: ctx.Mapper.Map<CreateUserResponse>(src.Data),
                        validationErrors: src.ValidationErrors,
                        message: src.Message,
                        httpCode: src.HttpCode,
                        status: src.Status
                    )
            );
        #endregion
        #region Login
        CreateMap<LoginRequest, User>();
        #endregion
    }
}
