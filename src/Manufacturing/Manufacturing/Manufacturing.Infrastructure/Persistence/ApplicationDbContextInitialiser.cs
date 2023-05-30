using Manufacturing.Domain.Entities;
using Manufacturing.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Manufacturing.Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContextInitialiser(
            ILogger<ApplicationDbContextInitialiser> logger,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Default roles
            var administratorRole = new IdentityRole("Administrator");

            if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await _roleManager.CreateAsync(administratorRole);
            }

            // Default users
            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (_userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await _userManager.CreateAsync(administrator, "Administrator1!");
                if (!string.IsNullOrWhiteSpace(administratorRole.Name))
                {
                    await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
                }
            }

            // Default data
            var employees = new Employee[]
            {
                new Employee
                {
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Position = "Начальник производства",
                    HireDate = DateTime.Parse("2015-09-01")
                },
                new Employee
                {
                    FirstName = "Петр",
                    LastName = "Петров",
                    Position = "Оператор линии",
                    HireDate = DateTime.Parse("2016-10-01")
                },
                new Employee
                {
                    FirstName = "Сергей",
                    LastName = "Сергеев",
                    Position = "Контролер качества",
                    HireDate = DateTime.Parse("2017-08-01")
                },
            };
            _context.Employees.AddRange(employees);
            await _context.SaveChangesAsync();

            // Добавление процессов производства
            var processes = new ProductionProcess[]
            {
                new ProductionProcess
                {
                    Name = "Проверка качества сырья",
                    Description = "Проверка качества ткани, резиновых материалов, фитингов и т.д.",
                    Duration = TimeSpan.FromHours(1)
                },
                new ProductionProcess
                {
                    Name = "Раскрой ткани",
                    Description = "Раскрой ткани и других материалов для изготовления рукава",
                    Duration = TimeSpan.FromHours(2)
                },
                new ProductionProcess
                {
                    Name = "Производство внутреннего резинового слоя",
                    Description = "Изготовление внутреннего резинового слоя рукава",
                    Duration = TimeSpan.FromHours(3)
                },
                new ProductionProcess
                {
                    Name = "Производство наружного защитного слоя",
                    Description = "Изготовление наружного текстильного или резинового слоя рукава",
                    Duration = TimeSpan.FromHours(4)
                },
                new ProductionProcess
                {
                    Name = "Изготовление фитингов",
                    Description = "Изготовление фитингов и присоединительных элементов для рукава",
                    Duration = TimeSpan.FromHours(2)
                },
                new ProductionProcess
                {
                    Name = "Сборка рукава",
                    Description = "Соединение внутреннего и наружного слоев рукава, установка фитингов и присоединительных элементов",
                    Duration = TimeSpan.FromHours(3)
                },
                new ProductionProcess
                {
                    Name = "Технический контроль и испытания",
                    Description = "Проведение гидравлических испытаний, проверка износа и стойкости к истиранию, гибкости и маневренности рукава",
                    Duration = TimeSpan.FromHours(2)
                },
                new ProductionProcess
                {
                    Name = "Упаковка и хранение",
                    Description = "Упаковка готовых рукавов и размещение их на складе",
                    Duration = TimeSpan.FromHours(1)
                },
            };
            _context.ProductionProcesses.AddRange(processes);
            await _context.SaveChangesAsync();

            // Добавление линий производства
            var lines = new ProductionLine[]
            {
                new ProductionLine
                {
                    Name = "Линия подготовки сырья",
                    Capacity = 100
                },
                new ProductionLine
                {
                    Name = "Линия изготовления основных элементов рукава",
                    Capacity = 80
                },
                new ProductionLine
                {
                    Name = "Линия сборки рукава",
                    Capacity = 50
                },
                new ProductionLine
                {
                    Name = "Линия технического контроля и испытаний",
                    Capacity = 40
                },

                new ProductionLine
                {
                    Name = "Линия упаковки и хранения",
                    Capacity = 100
                },
            };
            _context.ProductionLines.AddRange(lines);
            await _context.SaveChangesAsync();

            // Добавление продукции
            var products = new Product[]
            {
                new Product
                {
                    Name = "Пожарный рукав 1,5\"",
                    Description = "Пожарный рукав диаметром 1,5 дюйма с текстильным покрытием и стандартными фитингами",
                    CreationDate = DateTime.Parse("2021-01-01"),
                    QualityStatus = "OK"
                },
                new Product
                {
                    Name = "Пожарный рукав 2\"",
                    Description = "Пожарный рукав диаметром 2 дюйма с резиновым покрытием и стандартными фитингами",
                    CreationDate = DateTime.Parse("2021-02-01"),
                    QualityStatus = "OK"

                },
            };
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            // Добавление заказов на производство
            var orders = new ProductionOrder[]
            {
                new ProductionOrder
                {
                    OrderDate = DateTime.Parse("2021-01-01"),
                    Quantity = 100, Deadline = DateTime.Parse("2021-01-15"),
                    EmployeeId = employees.Single(e => e.LastName == "Иванов").EmployeeId
                },
                new ProductionOrder
                {
                    OrderDate = DateTime.Parse("2021-02-01"),
                    Quantity = 200, Deadline = DateTime.Parse("2021-02-15"),
                    EmployeeId = employees.Single(e => e.LastName == "Иванов").EmployeeId
                },
            };
            _context.ProductionOrders.AddRange(orders);
            await _context.SaveChangesAsync();

            // Добавление выполнения процессов
            var processExecutions = new ProcessExecution[]
            {
                new ProcessExecution
                {
                    StartTime = DateTime.Parse("2021-01-01 08:00"),
                    EndTime = DateTime.Parse("2021-01-01 09:00"),
                    EmployeeId = employees.Single(e => e.LastName == "Петров").EmployeeId,
                    ProcessId = processes.Single(p => p.Name == "Проверка качества сырья").ProcessId,
                    ProductId = products.Single(pr => pr.Name == "Пожарный рукав 1,5\"").ProductId,
                    LineId = lines.Single(l => l.Name == "Линия подготовки сырья").LineId
                },
                new ProcessExecution
                {
                    StartTime = DateTime.Parse("2021-02-01 08:00"),
                    EndTime = DateTime.Parse("2021-02-01 09:00"),
                    EmployeeId = employees.Single(e => e.LastName == "Петров").EmployeeId,
                    ProcessId = processes.Single(p => p.Name == "Проверка качества сырья").ProcessId,
                    ProductId = products.Single(pr => pr.Name == "Пожарный рукав 2\"").ProductId,
                    LineId = lines.Single(l => l.Name == "Линия подготовки сырья").LineId
                },
            };
            _context.ProcessExecutions.AddRange(processExecutions);
            await _context.SaveChangesAsync();

            // Добавление контроля качества
            var qualityControls = new QualityControl[]
            {
                new QualityControl
                {
                    ControlDate = DateTime.Parse("2021-01-15"),
                    Result = "OK", EmployeeId = employees.Single(e => e.LastName == "Сергеев").EmployeeId,
                    ProductId = products.Single(pr => pr.Name == "Пожарный рукав 1,5\"").ProductId
                },
                new QualityControl
                {
                    ControlDate = DateTime.Parse("2021-02-15"),
                    Result = "OK", EmployeeId = employees.Single(e => e.LastName == "Сергеев").EmployeeId,
                    ProductId = products.Single(pr => pr.Name == "Пожарный рукав 2\"").ProductId
                },
            };
            _context.QualityControls.AddRange(qualityControls);
            await _context.SaveChangesAsync();
        }
    }
}
