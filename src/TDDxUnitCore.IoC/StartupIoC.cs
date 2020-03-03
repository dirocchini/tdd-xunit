
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Audiences;
using TDDxUnitCore.Domain.Courses;
using TDDxUnitCore.Domain.Students;
using TDDxUnitCore.Persistence.Contexts;
using TDDxUnitCore.Persistence.Repositories;

namespace TDDxUnitCore.IoC
{
    public static class StartupIoC
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TddXUnitContext>(options => options.UseSqlServer(configuration["ConnectionString"]));

            services.AddScoped(typeof(IRepositoryBase<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IAudienceConverter), typeof(AudienceConverter));
            

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();


            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddScoped<CourseService>();
            services.AddScoped<StudentService>();
        }
    }
}