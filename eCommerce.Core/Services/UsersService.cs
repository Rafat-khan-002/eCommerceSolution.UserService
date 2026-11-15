using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entites;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services;

internal class UsersService : IUsersService
{
  private readonly IUsersRepository _usersRepository;
  private readonly IMapper _mapper;

  public UsersService(IUsersRepository usersRepository, IMapper mapper)
  {
    _usersRepository = usersRepository;
    _mapper = mapper;
  }


  public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
  {
    ApplicationUser? user = await _usersRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

    if (user == null)
    {
      return null;
    }

    //return new AuthenticationResponse(user.UserID, user.Email, user.PersonName, user.Gender, "token", Success: true);
    return _mapper.Map<AuthenticationResponse>(user) with { Success = true, Token = "token" };
  }




    public async Task<ServiceResponse<AuthenticationResponse>> Register(RegisterRequest registerRequest)
    {

        ApplicationUser user =_mapper.Map<ApplicationUser>(registerRequest);
        return await _usersRepository.AddUser(user);

    }


}
