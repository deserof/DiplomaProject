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
                    Name = "Закупка сырья и материалов",
                    Description = "Предприятия закупают сырье и материалы для производства своей продукции",
                    Duration = TimeSpan.FromHours(1)
                },
                new ProductionProcess
                {
                    Name = "Обработка сырья",
                    Description = "Сырье обрабатывается и преобразуется в промежуточные или конечные продукты. Это может включать механическую, химическую или физическую обработку",
                    Duration = TimeSpan.FromHours(2)
                },
                new ProductionProcess
                {
                    Name = "Сборка",
                    Description = "Промежуточные продукты собираются вместе для создания конечного продукта.",
                    Duration = TimeSpan.FromHours(3)
                },
                new ProductionProcess
                {
                    Name = "Контроль качества",
                    Description = "Продукция проверяется на соответствие стандартам качества и требованиям заказчика.",
                    Duration = TimeSpan.FromHours(4)
                },
                new ProductionProcess
                {
                    Name = "Упаковка и хранение",
                    Description = "Продукты упаковываются и хранятся до момента отправки заказчику или дистрибьютору.",
                    Duration = TimeSpan.FromHours(2)
                },
                new ProductionProcess
                {
                    Name = "Логистика и транспортировка",
                    Description = "Продукция транспортируется от предприятия к заказчикам или дистрибьюторам.",
                    Duration = TimeSpan.FromHours(3)
                },
                new ProductionProcess
                {
                    Name = "Обслуживание и поддержка",
                    Description = "Послепродажное обслуживание и поддержка продукции обеспечивается для удовлетворения потребностей клиентов и поддержания долгосрочных отношений.",
                    Duration = TimeSpan.FromHours(2)
                },
                new ProductionProcess
                {
                    Name = "Управление и планирование",
                    Description = "Управление производственными процессами, планирование ресурсов и координация между различными отделами предприятия.",
                    Duration = TimeSpan.FromHours(1)
                },
            };
            _context.ProductionProcesses.AddRange(processes);
            await _context.SaveChangesAsync();

            // Добавление продукции
            var products = new Product[]
            {
                new Product
                {
                    Name = "Теплообменник типа A",
                    Description = "Пластинчатый теплообменник с высокой эффективностью для отопительных систем",
                    CreationDate = DateTime.Parse("2021-01-01"),
                    QualityStatus = "OK"
                },
                new Product
                {
                    Name = "Теплообменник типа B",
                    Description = "Трубчатый теплообменник для использования в промышленных системах охлаждения",
                    CreationDate = DateTime.Parse("2021-02-01"),
                    QualityStatus = "OK"
                },
                new Product
                {
                    Name = "Теплообменник типа C",
                    Description = "Кожухотрубный теплообменник для использования в системах кондиционирования воздуха",
                    CreationDate = DateTime.Parse("2021-03-01"),
                    QualityStatus = "OK"
                },
                new Product
                {
                    Name = "Теплообменник типа D",
                    Description = "Кожухотрубный теплообменник для использования в системах кондиционирования воздуха",
                    CreationDate = DateTime.Parse("2020-03-01"),
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
                    EmployeeId = employees.Single(e => e.LastName == "Иванов").Id
                },
                new ProductionOrder
                {
                    OrderDate = DateTime.Parse("2021-02-01"),
                    Quantity = 200, Deadline = DateTime.Parse("2021-02-15"),
                    EmployeeId = employees.Single(e => e.LastName == "Иванов").Id
                },
                new ProductionOrder
                {
                    OrderDate = DateTime.Parse("2021-03-01")
                    , Quantity = 150, Deadline = DateTime.Parse("2021-03-15"),
                    EmployeeId = employees.Single(e => e.LastName == "Иванов").Id
                },
                new ProductionOrder
                {
                    OrderDate = DateTime.Parse("2021-04-01"),
                    Quantity = 250, Deadline = DateTime.Parse("2021-04-15"),
                    EmployeeId = employees.Single(e => e.LastName == "Иванов").Id
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
                    Description = "Замена сальника",
                    ProcessId = processes.Single(p => p.Name == "Обслуживание и поддержка").Id,
                    ProductId = products.Single(pr => pr.Name == "Теплообменник типа A").Id,
                },
                new ProcessExecution
                {
                    StartTime = DateTime.Parse("2021-02-01 08:00"),
                    EndTime = DateTime.Parse("2021-02-01 09:00"),
                    Description = "Сборка крышки",
                    ProcessId = processes.Single(p => p.Name == "Сборка").Id,
                    ProductId = products.Single(pr => pr.Name == "Теплообменник типа A").Id,
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
                    Result = "OK",
                    Comment = "Все параметры соответствуют стандартам",
                    EmployeeId = employees.Single(e => e.LastName == "Сергеев").Id,
                    ProductId = products.Single(pr => pr.Name == "Теплообменник типа A").Id
                },
                new QualityControl
                {
                    ControlDate = DateTime.Parse("2021-02-15"),
                    Result = "OK",
                    Comment = "Все параметры соответствуют стандартам",
                    EmployeeId = employees.Single(e => e.LastName == "Сергеев").Id,
                    ProductId = products.Single(pr => pr.Name == "Теплообменник типа A").Id
                },
                new QualityControl
                {
                    ControlDate = DateTime.Parse("2021-03-15"),
                    Result = "OK",
                    Comment = "Все параметры соответствуют стандартам",
                    EmployeeId = employees.Single(e => e.LastName == "Сергеев").Id,
                    ProductId = products.Single(pr => pr.Name == "Теплообменник типа A").Id
                },
                new QualityControl
                {
                    ControlDate = DateTime.Parse("2021-04-15"),
                    Result = "OK",
                    Comment = "Все параметры соответствуют стандартам",
                    EmployeeId = employees.Single(e => e.LastName == "Сергеев").Id,
                    ProductId = products.Single(pr => pr.Name == "Теплообменник типа B").Id
                },
            };
            _context.QualityControls.AddRange(qualityControls);
            await _context.SaveChangesAsync();
        }
    }
}
