using Cms.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Cms.Infrastructure.Persistence;

public class CmsDbContextSeed
{
    private readonly ILogger _logger;
    private readonly CmsDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public CmsDbContextSeed(
        ILogger logger, 
        CmsDbContext context, 
        UserManager<User> userManager,
        RoleManager<Role> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    
    public async Task SeedAsync()
    {
        try
        {
            //await SeedProducts();
            await SeedRoles();
            await SeedUsers();
            await SeedCategories();
            await SeedTags();
            await SeedPosts();
            await SeedSettings();
            await SeedFAQs();

            await SeedSiteContents();

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    #region SeedRoles

    private async Task SeedRoles()
    {
        if (!_roleManager.Roles.Any())
        {
            var roles = new List<Role>()
            {
                new()
                {
                    Name = "Admin",
                    RoleDesc = "Admin",
                    Order = 1,
                    IsVisible = true,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new()
                {
                    Name = "Publisher",
                    RoleDesc = "Publisher",
                    Order = 1,
                    IsVisible = true,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new()
                {
                    Name = "Editor",
                    RoleDesc = "Editor",
                    Order = 1,
                    IsVisible = true,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new()
                {
                    Name = "Customer",
                    RoleDesc = "Customer",
                    Order = 1,
                    IsVisible = true,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            };
            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }
            
            //await _context.SaveChangesAsync();
        }
    }

    #endregion

    #region SeedUsers

    private async Task SeedUsers()
    {
        if (!_userManager.Users.Any())
        {
            string defaultPassword = "AnhKien@2024";
            User u1 = new()
            {
                UserName = "buuhq",
                Email = "buuhq@gmail.com",
                AuthType = 'D',
                Fullname = "Buu HQ",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var aaa = await _userManager.CreateAsync(u1, defaultPassword);
            // await _userManager.AddClaimAsync(moviUser, new Claim("Fullname", moviUser.Fullname));
            await _userManager.AddToRolesAsync(
                u1, new string[] {"Admin", "Editor", "Publisher", "Customer"}
            );
            
            User u2 = new()
            {
                UserName = "Admin",
                Email = "admin@gmail.com",
                AuthType = 'D',
                Fullname = "Admin",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            await _userManager.CreateAsync(u2, defaultPassword);
            //await _userManager.AddClaimAsync(moviUser, new Claim("Fullname", moviUser.Fullname));
            await _userManager.AddToRolesAsync(
                u2, new string[] {"Admin"}
            );
            
            User u3 = new()
            {
                UserName = "Editor",
                Email = "editor@gmail.com",
                AuthType = 'D',
                Fullname = "Editor",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            await _userManager.CreateAsync(u3, defaultPassword);
            await _userManager.AddToRolesAsync(
                u3, new string[] {"Editor"}
            );
            
            User u4 = new()
            {
                UserName = "Publisher",
                Email = "publisher@gmail.com",
                AuthType = 'D',
                Fullname = "Publisher",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            await _userManager.CreateAsync(u4, defaultPassword);
            await _userManager.AddToRolesAsync(
                u4, new string[] {"Publisher"}
            );
            
            User u5 = new()
            {
                UserName = "Customer",
                Email = "customer@gmail.com",
                AuthType = 'D',
                Fullname = "Customer",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            await _userManager.CreateAsync(u5, defaultPassword);
            await _userManager.AddToRolesAsync(
                u5, new string[] {"Customer"}
            );
            
            //await _context.SaveChangesAsync();
            
        }
    }

    #endregion
    
    #region SeedProducts
    //private async Task SeedProducts()
    //{
    //    if (!_context.Products.Any())
    //    {
    //        var products = new List<Product>
    //        {
    //            new()
    //            {
    //                No = "Lotus",
    //                Name = "Esprit",
    //                Summary = "Nondisplaced fracture of greater trochanter of right femur",
    //                Description = "Nondisplaced fracture of greater trochanter of right femur",
    //                Price = (decimal)177940.49
    //            },
    //            new()
    //            {
    //                No = "Cadillac",
    //                Name = "CTS",
    //                Summary = "Carbuncle of trunk",
    //                Description = "Carbuncle of trunk",
    //                Price = (decimal)114728.21
    //            }
    //        };
    //        await _context.Products.AddRangeAsync(products);
    //    }
    //}
    #endregion

    #region SeedCategories
    private async Task SeedCategories()
    {
        if (!_context.Categories.Any())
        {
            var categories = new List<Category>()
            {
                // new()
                // {
                //     Title = "Trang chủ",
                //     Content = "Trang chủ",
                //     Slug = "home"
                // },
                new()
                {
                    Title = "Tin Thám tử",
                    Content = "Các thông tin trong lĩnh vực Thám tử",
                    Slug = "tham-tu",
                    IsVisible = true
                },
                new()
                {
                    Title = "default",
                    Content = "default",
                    Slug = "default",
                    IsVisible = true
                }
            };
            await _context.Categories.AddRangeAsync(categories);
            await _context.SaveChangesAsync();
        }
    }
    #endregion
    
    #region SeedTags

    private async Task SeedTags()
    {
        if (!_context.Tags.Any())
        {
            var tags = new List<Tag>()
            {
                // new()
                // {
                //     Title = "AboutUs",
                //     Slug = "about-us",
                //     IsVisible = false
                // },
                // new()
                // {
                //     Title = "Dịch vụ",
                //     Slug = "services",
                //     IsVisible = false
                // },
                new()
                {
                    Title = "Thám Tử",
                    Slug = "tham-tu",
                    IsVisible = true
                },
                new()
                {
                    Title = "default",
                    Slug = "default",
                    IsVisible = true
                }
            };
            await _context.Tags.AddRangeAsync(tags);
            await _context.SaveChangesAsync();
        }
    }

    #endregion


    #region SeedPosts

    private async Task SeedPosts()
    {
        if (!_context.Posts.Any())
        {
//            var aboutUsPost = new Post()
//            {
//                Author = "buuhq",
//                Title = "CHÚNG TÔI LÀ AI?",
//                ImageUrl = "",
//                Slug = "about-us.html",
//                IsPublished = true,
//                Summary = @"
//    <div class=""col-lg-6"">
//            <p>
//                Chúng tôi là đội ngũ thám tử có dày dặn kinh nghiệp có nghiệp cụ cao, đầu tư các trong thiết bị hiện đại để cung cấp tới khách các các dịch vụ thám tử đảm bảo:
//            </p>
//            <ul>
//                <li><i class=""ri-check-double-line""></i> Tính bảo mật tuyệt đối</li>
//                <li><i class=""ri-check-double-line""></i> Nhanh chóng và hợp pháp</li>
//                <li><i class=""ri-check-double-line""></i> Chi phí vô cùng hợp lý</li>
//            </ul>
//        </div>
//        <div class=""col-lg-6 pt-4 pt-lg-0"">
//            <p>
//                Hiện tại chúng tôi có hệ thống chi nhánh rộng khắp cả nước trải rộng 3 miền Bắc, Trung, Nam.
//            </p>
            
//    </div>
//",
//                Content = @"
//    <p>
//        Chúng tôi luôn thấu hiển và đồng hành cùng khách hàng để tìm về các giá trị đáp ứng được kỳ vọng.
//    </p>
//    <div>
//        <p>
//            Chúng tôi sở hữu đội ngũ thám tử có dày dặn kinh nghiệp có nghiệp cụ cao, đầu tư các trong thiết bị hiện đại để cung cấp tới khách các các dịch vụ thám tử đảm bảo:
//        </p>
//        <ul>
//            <li><i class=""ri-check-double-line""></i> Tính bảo mật tuyệt đối</li>
//            <li><i class=""ri-check-double-line""></i> Nhanh chóng và hợp pháp</li>
//            <li><i class=""ri-check-double-line""></i> Chi phí vô cùng hợp lý</li>
//        </ul>
//    </div>
//    <p>Hiện tại chúng tôi có văn phòng đại diện tại 3 miền Bắc, Trung, Nam cung cấp các dịch vụ thám tử trên phạm vi toàn quốc</p>
//",
//                Categories = _context.Categories.Where(x => x.Slug.Equals("home")).ToList(),
//                Tags = _context.Tags.Where(x => x.Slug.Equals("about-us")).ToList(),
//                Order = 1,
            
//            };
            
//            var servicePosts = new List<Post>()
//            {
//                //---------------------------
//                new()
//                {
//                    Author = "buuhq",
//                    Title = "Theo dõi ngoại tình",
//                    ImageUrl = "",
//                    Slug = "theo-doi-ngoai-tinh.html",
//                    IsPublished = true,
//                    Summary = @"
//<p>Thông tin được giữ tuyệt mật, thu thập các thông tin theo yêu cầu, tư vấn pháp lý ra quyết định.</p>
//",
//                    Content = @"
//<p>Thông tin được giữ tuyệt mật, thu thập các thông tin theo yêu cầu, tư vấn pháp lý ra quyết định.</p>
//",
//                    Categories =  _context.Categories.Where(x => x.Slug.Equals("home")).ToList(),
//                    Tags = _context.Tags.Where(x => x.Slug.Equals("services")).ToList(),
//                    Order = 1
//                },
//                //---------------------------
//                new()
//                {
//                    Author = "buuhq",
//                    Title = "Theo dõi con cái",
//                    ImageUrl = "",
//                    Slug = "theo-doi-con-cai.html",
//                    IsPublished = true,
//                    Summary = @"
//<p>Theo dõi hoạt động hành vi con cái, giúp các bậc phụ huynh đánh giá hành vi, can thiệp hợp lý</p>
//",
//                    Content = @"
//<p>Theo dõi hoạt động hành vi con cái, giúp các bậc phụ huynh đánh giá hành vi, can thiệp hợp lý</p>
//",
//                    Categories =  _context.Categories.Where(x => x.Slug.Equals("home")).ToList(),
//                    Tags = _context.Tags.Where(x => x.Slug.Equals("services")).ToList(),
//                    Order = 2,
                    
//                },
//                //---------------------------
//                new()
//                {
//                    Author = "buuhq",
//                    Title = "Điều tra huyết thống",
//                    ImageUrl = "",
//                    Slug = "dieu-tra-huyet-thong.html",
//                    IsPublished = true,
//                    Summary = @"
//<p>Điều tra huyết thống cha con, mẹ con, ông cháu, bà cháu ...</p>
//",
//                    Content = @"
//<p>Điều tra huyết thống cha con, mẹ con, ông cháu, bà cháu ...</p>
//",
//                    Categories =  _context.Categories.Where(x => x.Slug.Equals("home")).ToList(),
//                    Tags = _context.Tags.Where(x => x.Slug.Equals("services")).ToList(),
//                    Order = 3,
                    
//                },
//                //---------------------------
//                new()
//                {
//                    Author = "buuhq",
//                    Title = "Điều tra số điện thoại",
//                    ImageUrl = "",
//                    Slug = "dieu-tra-so-dien-thoai.html",
//                    IsPublished = true,
//                    Summary = @"
//<p>Truy tìm chủ nhân số điện thoại</p>
//",
//                    Content = @"
//<p>Truy tìm chủ nhân số điện thoại</p>
//",
//                    Categories =  _context.Categories.Where(x => x.Slug.Equals("home")).ToList(),
//                    Tags = _context.Tags.Where(x => x.Slug.Equals("services")).ToList(),
//                    Order = 4,
                    
//                },
//                //---------------------------
//                new()
//                {
//                    Author = "buuhq",
//                    Title = "Điều tra biển số xe",
//                    ImageUrl = "",
//                    Slug = "dieu-tra-bien-so-xe.html",
//                    IsPublished = true,
//                    Summary = @"
//<p>Điều tra thông tin theo bản số xe</p>
//",
//                    Content = @"
//<p>Điều tra thông tin theo bản số xe</p>
//",
//                    Categories =  _context.Categories.Where(x => x.Slug.Equals("home")).ToList(),
//                    Tags = _context.Tags.Where(x => x.Slug.Equals("services")).ToList(),
//                    Order = 5,
                   
//                },
//                //---------------------------
//                new()
//                {
//                    Author = "buuhq",
//                    Title = "Điều tra hàng giả",
//                    ImageUrl = "",
//                    Slug = "dieu-tra-hang-gia.html",
//                    IsPublished = true,
//                    Summary = @"
//<p>Điều tra nguồn gốc hàng giả, hàng nháy</p>
//",
//                    Content = @"
//<p>Điều tra nguồn gốc hàng giả, hàng nháy</p>
//",
//                    Categories =  _context.Categories.Where(x => x.Slug.Equals("home")).ToList(),
//                    Tags = _context.Tags.Where(x => x.Slug.Equals("services")).ToList(),
//                    Order = 6,
                    
//                },
//                //---------------------------
//                new()
//                {
//                    Author = "buuhq",
//                    Title = "Điều tra trộm cắp trong doanh nghiệp",
//                    ImageUrl = "",
//                    Slug = "dieu-tra-trom-cap.html",
//                    IsPublished = true,
//                    Summary = @"
//<p>Điều tra các vụ việc trộm cắp trong doanh nghiệp</p>
//",
//                    Content = @"
//<p>Điều tra các vụ việc trộm cắp trong doanh nghiệp</p>
//",
//                    Categories =  _context.Categories.Where(x => x.Slug.Equals("home")).ToList(),
//                    Tags = _context.Tags.Where(x => x.Slug.Equals("services")).ToList(),
//                    Order = 7,
                    
//                },
//                //---------------------------
//                new()
//                {
//                    Author = "buuhq",
//                    Title = "Điều tra đối thủ cạnh tranh",
//                    ImageUrl = "",
//                    Slug = "dieu-tra-doi-thu-canh-tranh.html",
//                    IsPublished = true,
//                    Summary = @"
//<p>Điều tra thông tin về đối thủ cạnh tranh để có các điều chiến lược phù hợp cho doanh nghiệp</p>
//",
//                    Content = @"
//<p>Điều tra thông tin về đối thủ cạnh tranh để có các điều chiến lược phù hợp cho doanh nghiệp</p>
//",
//                    Categories =  _context.Categories.Where(x => x.Slug.Equals("home")).ToList(),
//                    Tags = _context.Tags.Where(x => x.Slug.Equals("services")).ToList(),
//                    Order = 8,
                    
//                },
//            };

            var blogPosts = new List<Post>()
            {
                new ()
                {
                    Author = "buuhq",
                    Title = "Walter White",
                    ImageUrl = "/assets/arsha/img/team/team-1.jpg",
                    Slug = "walter-while.html",
                    IsPublished = true,
                    Summary = @"Explicabo voluptatem mollitia et repellat qui dolorum quasi",
                    Content = @"Explicabo voluptatem mollitia et repellat qui dolorum quasi",
                    Categories =  _context.Categories.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                    Tags = _context.Tags.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                    Order = 1,
                   
                },
                //---------------------------
                new ()
                {
                    Author = "buuhq",
                    Title = "Sarah Jhonson",
                    ImageUrl = "/assets/arsha/img/team/team-2.jpg",
                    Slug = "sarah-jhonson.html",
                    IsPublished = true,
                    Summary = @"Aut maiores voluptates amet et quis praesentium qui senda para",
                    Content = @"Aut maiores voluptates amet et quis praesentium qui senda para",
                    Categories =  _context.Categories.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                    Tags = _context.Tags.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                    Order = 2,
                   
                },
                //----------------------
                new ()
                {
                    Author = "buuhq",
                    Title = "William Anderson",
                    ImageUrl = "/assets/arsha/img/team/team-3.jpg",
                    Slug = "william-anderson.html",
                    IsPublished = true,
                    Summary = @"Quisquam facilis cum velit laborum corrupti fuga rerum quia",
                    Content = @"Quisquam facilis cum velit laborum corrupti fuga rerum quia",
                    Categories =  _context.Categories.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                    Tags = _context.Tags.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                    Order = 3,
                    
                },
                //----------------------
                new ()
                {
                    Author = "buuhq",
                    Title = "Amanda Jepson",
                    ImageUrl = "/assets/arsha/img/team/team-4.jpg",
                    Slug = "amanda-jepson.html",
                    IsPublished = true,
                    Summary = @"Dolorum tempora officiis odit laborum officiis et et accusamus",
                    Content = @"Dolorum tempora officiis odit laborum officiis et et accusamus",
                    Categories =  _context.Categories.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                    Tags = _context.Tags.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                    Order = 4,
                    
                },
            };

            //await _context.Posts.AddAsync(aboutUsPost);
            //await _context.Posts.AddRangeAsync(servicePosts);
            await _context.Posts.AddRangeAsync(blogPosts);
            await _context.SaveChangesAsync();
        }
    }

    #endregion

    #region SeedSettings

    private async Task SeedSettings()
    {
        if (!_context.Settings.Any())
        {
            var settings = new List<Setting>()
            {
                new()
                {
                    Key = "SiteName",
                    Value = "THÁM TỬ DAILY"
                },
                new()
                {
                    Key = "LogoImageUrl",
                    Value = "cms/tham-tu-logo.png"
                },
                new()
                {
                    Key = "HQAddress",
                    Value = "789 Hưng Phú"
                },
                new()
                {
                    Key = "HQWard",
                    Value = "Phường 10"
                },
                new()
                {
                    Key = "HQDistrict",
                    Value = "Quận 8"
                },
                new()
                {
                    Key = "HQProvince",
                    Value = "Tp. HCM"
                },
                new()
                {
                    Key = "HQPhone",
                    Value = "090 3456 789"
                },
                new()
                {
                    Key = "HQEmail",
                    Value = "info@thamtutu.net.vn"
                },
                new()
                {
                    Key = "HQGoogleMap",
                    Value = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2608.645849633463!2d106.66382456601767!3d10.745486122495203!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752f75b741821b%3A0x2b03930b952067b7!2zSG_DoCBLw70gTcOsIEdpYSAtIFPhu6dpIEPhuqNv!5e0!3m2!1sen!2s!4v1702072959148!5m2!1sen!2s"
                },
            };
            await _context.Settings.AddRangeAsync(settings);
        }
    }

    #endregion

    #region SeedFAQs

    private async Task SeedFAQs()
    {
        if (!_context.FAQs.Any())
        {
            var faqs = new List<FAQ>()
            {
                new ()
                {
                    Question = "Thông tin khách hàng cung cấp đảm bảo bí mật không? ",
                    Answer = "Feugiat pretium nibh ipsum consequat. Tempus iaculis urna id volutpat lacus laoreet non curabitur gravida. Venenatis lectus magna fringilla urna porttitor rhoncus dolor purus non.",
                    Position = 1
                },
                //---------------
                new ()
                {
                    Question = "Chi chí dịch vụ ra sao?",
                    Answer = "Dolor sit amet consectetur adipiscing elit pellentesque habitant morbi. Id interdum velit laoreet id donec ultrices. Fringilla phasellus faucibus scelerisque eleifend donec pretium. Est pellentesque elit ullamcorper dignissim. Mauris ultrices eros in cursus turpis massa tincidunt dui.",
                    Position = 2
                },
                //---------------
                new ()
                {
                    Question = "Đăng ký dịch vụ như thế nào?",
                    Answer = "Eleifend mi in nulla posuere sollicitudin aliquam ultrices sagittis orci. Faucibus pulvinar elementum integer enim. Sem nulla pharetra diam sit amet nisl suscipit. Rutrum tellus pellentesque eu tincidunt. Lectus urna duis convallis convallis tellus. Urna molestie at elementum eu facilisis sed odio morbi quis.",
                    Position = 3
                },
                //---------------
                new ()
                {
                    Question = "Thời gian xử lý trung bình cho mỗi vụ việc?",
                    Answer = "Molestie a iaculis at erat pellentesque adipiscing commodo. Dignissim suspendisse in est ante in. Nunc vel risus commodo viverra maecenas accumsan. Sit amet nisl suscipit adipiscing bibendum est. Purus gravida quis blandit turpis cursus in.",
                    Position = 4
                },
                //---------------
                new ()
                {
                    Question = "Có các cam kết chất lượng dịch vụ nào không?",
                    Answer = "Laoreet sit amet cursus sit amet dictum sit amet justo. Mauris vitae ultricies leo integer malesuada nunc vel. Tincidunt eget nullam non nisi est sit amet. Turpis nunc eget lorem dolor sed. Ut venenatis tellus in metus vulputate eu scelerisque.",
                    Position = 5
                }
            };
            await _context.FAQs.AddRangeAsync(faqs);
        }
    }

    #endregion


    private async Task SeedSiteContents()
    {
        if (!_context.SiteContents.Any())
        {
            var siteContents = new List<SiteContent>()
            {
                new SiteContent()
                {
                    ContentType = "AboutUs",
                    Title = "CHÚNG TÔI LÀ AI?",
                    ImageUrl = "",
                    Slug = "about-us.html",
                    Summary = @"
    <div class=""col-lg-6"">
            <p>
                Chúng tôi là đội ngũ thám tử có dày dặn kinh nghiệp có nghiệp cụ cao, đầu tư các trong thiết bị hiện đại để cung cấp tới khách các các dịch vụ thám tử đảm bảo:
            </p>
            <ul>
                <li><i class=""ri-check-double-line""></i> Tính bảo mật tuyệt đối</li>
                <li><i class=""ri-check-double-line""></i> Nhanh chóng và hợp pháp</li>
                <li><i class=""ri-check-double-line""></i> Chi phí vô cùng hợp lý</li>
            </ul>
        </div>
        <div class=""col-lg-6 pt-4 pt-lg-0"">
            <p>
                Hiện tại chúng tôi có hệ thống chi nhánh rộng khắp cả nước trải rộng 3 miền Bắc, Trung, Nam.
            </p>
            
    </div>
",
                Content = @"
    <p>
        Chúng tôi luôn thấu hiển và đồng hành cùng khách hàng để tìm về các giá trị đáp ứng được kỳ vọng.
    </p>
    <div>
        <p>
            Chúng tôi sở hữu đội ngũ thám tử có dày dặn kinh nghiệp có nghiệp cụ cao, đầu tư các trong thiết bị hiện đại để cung cấp tới khách các các dịch vụ thám tử đảm bảo:
        </p>
        <ul>
            <li><i class=""ri-check-double-line""></i> Tính bảo mật tuyệt đối</li>
            <li><i class=""ri-check-double-line""></i> Nhanh chóng và hợp pháp</li>
            <li><i class=""ri-check-double-line""></i> Chi phí vô cùng hợp lý</li>
        </ul>
    </div>
    <p>Hiện tại chúng tôi có văn phòng đại diện tại 3 miền Bắc, Trung, Nam cung cấp các dịch vụ thám tử trên phạm vi toàn quốc</p>
",
                
                Order = 1,
                },

                //---------------------------
                new()
                {
                    ContentType = "Service",
                    Title = "Dịch vụ Theo dõi ngoại tình",
                    ImageUrl = "",
                    Slug = "theo-doi-ngoai-tinh.html",
                    Summary = @"
<p>Thông tin được giữ tuyệt mật, thu thập các thông tin theo yêu cầu, tư vấn pháp lý ra quyết định.</p>
",
                    Content = @"
<p>Thông tin được giữ tuyệt mật, thu thập các thông tin theo yêu cầu, tư vấn pháp lý ra quyết định.</p>
",
                   
                    Order = 1
                },
                //---------------------------
                new()
                {
                    ContentType = "Service",
                    Title = "Theo dõi con cái",
                    ImageUrl = "",
                    Slug = "theo-doi-con-cai.html",
                    Summary = @"
<p>Theo dõi hoạt động hành vi con cái, giúp các bậc phụ huynh đánh giá hành vi, can thiệp hợp lý</p>
",
                    Content = @"
<p>Theo dõi hoạt động hành vi con cái, giúp các bậc phụ huynh đánh giá hành vi, can thiệp hợp lý</p>
",
     
                    Order = 2,

                },
                //---------------------------
                new()
                {
                    ContentType = "Service",
                    Title = "Dịch vụ Điều tra huyết thống",
                    ImageUrl = "",
                    Slug = "dieu-tra-huyet-thong.html",
                    
                    Summary = @"
<p>Điều tra huyết thống cha con, mẹ con, ông cháu, bà cháu ...</p>
",
                    Content = @"
<p>Điều tra huyết thống cha con, mẹ con, ông cháu, bà cháu ...</p>
",
                    Order = 3,

                },
                //---------------------------
                new()
                {
                    ContentType = "Service",
                    Title = "Dịch vụ Điều tra số điện thoại",
                    ImageUrl = "",
                    Slug = "dieu-tra-so-dien-thoai.html",                 
                    Summary = @"
<p>Truy tìm chủ nhân số điện thoại</p>
",
                    Content = @"
<p>Truy tìm chủ nhân số điện thoại</p>
",
                    
                    Order = 4,

                },
                //---------------------------
                new()
                {
                    ContentType = "Service",
                    Title = "Dịch vụ Điều tra biển số xe",
                    ImageUrl = "",
                    Slug = "dieu-tra-bien-so-xe.html",                   
                    Summary = @"
<p>Điều tra thông tin theo bản số xe</p>
",
                    Content = @"
<p>Điều tra thông tin theo bản số xe</p>
",
                    Order = 5,

                },
                //---------------------------
                new()
                {
                    ContentType = "Service",
                    Title = "Dich vụ Điều tra hàng giả",
                    ImageUrl = "",
                    Slug = "dieu-tra-hang-gia.html",                 
                    Summary = @"
<p>Điều tra nguồn gốc hàng giả, hàng nháy</p>
",
                    Content = @"
<p>Điều tra nguồn gốc hàng giả, hàng nháy</p>
",
                    
                    Order = 6,

                },
                //---------------------------
                new()
                {
                    ContentType = "Service",
                    Title = "Điều tra trộm cắp trong doanh nghiệp",
                    ImageUrl = "",
                    Slug = "dieu-tra-trom-cap.html",
                    Summary = @"
<p>Điều tra các vụ việc trộm cắp trong doanh nghiệp</p>
",
                    Content = @"
<p>Điều tra các vụ việc trộm cắp trong doanh nghiệp</p>
",
                    Order = 7,

                },
                //---------------------------
                new()
                {
                    ContentType = "Service",
                    Title = "Điều tra đối thủ cạnh tranh",
                    ImageUrl = "",
                    Slug = "dieu-tra-doi-thu-canh-tranh.html",
                    Summary = @"
<p>Điều tra thông tin về đối thủ cạnh tranh để có các điều chiến lược phù hợp cho doanh nghiệp</p>
",
                    Content = @"
<p>Điều tra thông tin về đối thủ cạnh tranh để có các điều chiến lược phù hợp cho doanh nghiệp</p>
",
                    Order = 8,

                },
            };
            await _context.SiteContents.AddRangeAsync(siteContents);
        }
    }
}
