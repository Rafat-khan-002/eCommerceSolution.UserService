using Dapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entites;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;

namespace eCommerce.Infrastructure.Repositories;

internal class UsersRepository : IUsersRepository
{
  private readonly DapperDbContext _dbContext;

  public UsersRepository(DapperDbContext dbContext)
  {
    _dbContext = dbContext;
  }

    public async Task<ServiceResponse<AuthenticationResponse>> AddUser(
        ApplicationUser user)
    {
        user.UserID = Guid.NewGuid();

        string query = @"
        CALL public.""sp_AddUser""
        (
            @UserID,
            @Email,
            @PersonName,
            @Gender,
            @Password,
            NULL
        );";

        string message = await _dbContext.DbConnection
            .QuerySingleAsync<string>(query, new
            {
                UserID = user.UserID,
                Email = user.Email,
                PersonName = user.PersonName,
                Gender = user.Gender,
                Password = user.Password
            });

        // Registration successful
        if (message == "Registration successful.")
        {
            return new ServiceResponse<AuthenticationResponse>
            {
                IsSuccess = true,
                //Message = message,
                Data = new AuthenticationResponse
                {
                    UserID = user.UserID,
                    Email = user.Email,
                    PersonName = user.PersonName,
                    Gender = user.Gender,
                    Success = true,
                    Token = null,
                    Message=""
                }
            };
        }

        // Registration failed
        return new ServiceResponse<AuthenticationResponse>
        {
            IsSuccess = false,
            //Message = message,
            Data = null,
            Error = new ErrorResponse(400, message)
        };
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
  {
    //SQL query to select a user by Email and Password
    string query = "SELECT * FROM public.\"Users\" WHERE \"Email\"=@Email AND \"Password\"=@Password";
    var parameters = new { Email = email, Password = password };

    ApplicationUser? user = await _dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);

    return user;
  }
}

