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


            var blogPosts = new List<Post>()
            {
                new ()
                {
                    Author = "buuhq",
                    Title = "Cách theo dõi chồng qua zalo",
                    ImageUrl = "/assets/arsha/img/team/team-1.jpg",
                    Slug = "theo-doi-chong-qua-zalo.html",
                    IsPublished = true,
                    Summary = @"Theo dõi Zalo của chồng trên điện thoại bằng cách nào không bị phát hiện?",
                    Content = @"
<p>Có phải các chị em đang quan tâm rất nhiều đến câu hỏi “Theo dõi Zalo của chồng trên điện thoại bằng cách nào không bị phát hiện? cách theo dõi chồng qua Zalo nào bí mật? Cách theo dõi Zalo của chồng miễn phí? ”. 
Đó là rất nhiều câu hỏi của chị em phụ nữ khi nghi ngờ chồng ngoại tình. 
Để giải đáp những câu hỏi này, các chị em hãy đọc bài viết dưới đây của thám tử tư Thăng Long nhé
</p>
<p>
Quyền riêng tư của người khác là không nên xâm phạm, tuy nhiên điều này cũng chỉ là để người vợ biết được chồng mình có lăng nhăng bên ngoài hay không và từ đó bảo vệ được chính hạnh phúc gia đình nhỏ của mình. 
Đồng cảm với tâm lý của người phụ nữ, chúng tôi – những chuyên gia tâm lý của công ty thám tử tư Daily sẽ bật mí cho các bạn một số cách theo dõi zalo của chồng bằng máy tính và điện thoại dưới đây.
</p>
<p>
Zalo hiện đang là ứng dụng có lượng người sử dụng rất đông đảo. 
Với thiết kế giao diện đơn giản, thân thiện giúp cho người dùng có thể sử dụng một cách dễ dàng. 
Hơn nữa việc sử dụng zalo có tính riêng tư rất cao, ít công khai thông tin hơn như trên nền tảng mạng xã hội Facebook.
</p>
<p>
Những bạn bè có trong danh bạ của zalo hầu như là những người quen biết với mình, chủ động kết bạn giao lưu. 
Zalo là một trong những ứng dụng rất hữu ích đem đến cho người dùng những chức năng cần thiết như nghe gọi, nhắn tin, giao lưu với bạn bè và trao đổi công việc.
</p>
<p>
Do đó, rất nhiều người vợ để ý chồng mình thông qua ứng dụng zalo và mong muốn tìm được cách theo dõi zalo của chồng bằng điện của mình. 
Nhờ vậy mà chị em có thể giám sát chồng mình mỗi khi các anh ra đi ngoài để có thể kịp thời phát hiện những hành động sai trái của chồng. 
Chị em cũng có thể dùng cách này để điều tra khi cảm thấy chồng có dấu hiệu ngoại tình.
</p>
<p>
Tuy nhiên, để truy cập vào ứng dụng Zalo của người khác lại là một chuyện khó khăn bởi ứng dụng này là tài sản cá nhân của mỗi người dùng. 
Vậy làm thế nào để chị em có thể bẻ khóa vào được zalo của chồng? Bởi khi bẻ khoá được Zalo của chồng thì bạn mới có cách theo dõi chồng qua Zalo.
</p>
<h5>Cách theo dõi Zalo của chồng bằng điện thoại của mình miễn phí</h5>
<h6>Đăng nhập ứng dụng bằng mã QR CODE</h6>
<p>
Sau một thời gian nâng cấp, ứng dụng Zalo ngoài đăng nhập bằng mật khẩu thông thường như trước, giờ đây đã tích hợp thêm tính năng đăng nhập bằng mã QR Code vô cùng tiện dụng. 
Quét mã QR là một lựa chọn giúp chị em có thể để âm thầm theo dõi chồng mình.
</p>
<p>
Giờ đây, bạn không cần phải biết mật khẩu của chồng mà vẫn có thể dễ dàng truy cập được vào ứng dụng Zalo. 
Chỉ với một vài thao tác đơn giản các chị em có thể lợi dụng “kẽ hở” này để kiểm tra các tin nhắn từ Zalo. 
Nhờ vậy, bằng một cách dễ dàng bạn có thể theo dõi bí mật từ Zalo chồng.
</p>

<h5>Thứ tự các bước đăng nhập bằng QR CODE:</h5>
<p>Bước 1: Bạn vào ứng dụng google chrome trên điện thoại (đối với dòng điện thoại Android) và ứng dụng safari trên điện thoại iphone.</p>
<p>Bước 2 : Bạn chuyển sang chế độ “Yêu cầu trang web cho máy tính”</p>
<p>Đối với dòng điện thoại sử dụng hệ điều hành Android thì ứng dụng google chrome có sẵn chức năng “Yêu cầu trang web cho máy tình”. 
Vì thế bạn chỉ cần mở ứng dụng google chrome lên và ấn vào nút 3 chấm và chọn.</p>
<p>Với điện thoại iphone thì sao? đối với điện thoại iphone thì ứng dụng google chrome không có sẵn chức năng “Yêu cầu trang web cho máy tình”. 
Vì thế, đối với iphone thì bạn mở ứng dụng Safari và ấn vào góc bên trái ở chữ AA và chọn “Yêu cầu trang web cho máy tính”</p>
<p>Bước 3: Truy cập vào trang web chat.zalo.me</p>
<p>Bước 4: Chọn mục quét mã QR bằng Zalo để đăng nhập.</p>
<p>Bước 5: Mở Zalo của chồng bạn lên –> Chọn vào mục quét mã QR CODE</p>
<p>Bước 6: Truy cập vào mục quét mã QR CODE và di chuyển camera trên máy của chồng bạn vào mã QR CODE trên điện thoại của bạn.</p>

",
                    Categories =  _context.Categories.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                    Tags = _context.Tags.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                    Order = 1,
                   
                },
                new ()
                {
                    Author = "buuhq",
                    Title = "Cách Định Vị IPHONE Của Chồng",
                    ImageUrl = "/assets/arsha/img/team/team-3.jpg",
                    Slug = "cach-dinh-vi-iphone-chong.html",
                    IsPublished = true,
                    Summary = @"Các cách định vị iphone chồng mà chị em cần phải biết",
                    Content = @"
<p>Cách định vị iphone của chồng đang là cụm từ được rất nhiều chị em quan tâm và tìm kiếm. 
Có nhiều lúc họ tò mò không biết rằng chồng mình đang ở đâu, làm gì, đi với ai. 
Mặc dù rất muốn biết nhưng họ lại chưa biết cách để biết được chính xác những điều trên.</p>
<p>Lúc này, điều mà các bà vợ cần làm là phải tìm cách quản lý được điện thoại của chồng mình từ xa để nắm bắt được thông tin mà không bị anh ấy phát hiện. Và ở bài viết này, Thám tư tư Daily sẽ chia sẻ cho các chị em phụ nữ biết một số cách để xác định vị trí người dùng iphone một cách đơn giản, thuận tiện và chính xác nhất. 
Xin mời bạn đọc hãy cùng theo dõi với thám tử tư Daily để biết thêm chi tiết!</p>

<h5>Lý do các chị em tìm cách định vị iphone của chồng</h5>
<p>Cuộc sống đi làm bận rộn ngày nay khiến cho thời gian hoạt động bên ngoài của các anh chồng khá là nhiều và có mối quan hệ xã hội khá rộng. 
Ngày nay, thời gian giao lưu hoạt động, đi làm của người chồng ở bên ngoài là khá nhiều.</p>
<p>Chính vì thế mà các chị em không thể tự theo dõi chồng mình từ nơi này đến nơi khác được. 
Điều này tốn rất nhiều thời gian và công sức. 
Ngoài ra, hành động cũng không mấy hiệu quả do đường xá đông đúc, khó theo sát và dễ bị người chồng phát hiện.</p>
<p>Hơn nữa bạn cũng không thể bỏ tất cả công việc của mình chỉ đi chạy theo giám sát anh ấy. Do vậy, để đơn giản hóa cũng như tiết kiệm được công sức và thời gian, thám tử Daily chúng tôi khuyên bạn hãy sử dụng cách định vị trên iphone của chồng bạn.</p>
<h5>Một số cách định vị iphone của chồng để theo dõi mà không bị phát hiện</h5>
<p>Ngày nay, công nghệ khoa học tiên tiến ngày càng phát triển và hiện đại. 
Việc xác định vị trí của ai đó đã trở thành một điều vô cùng dễ dàng trong thời đại 4.0 này.</p>
<p>Bạn muốn tìm cách định vị iPhone của chồng mình để xem các anh thường xuyên lui tới địa điểm nào nhưng lại không muốn chồng biết vì sợ anh không hài lòng và không muốn bị kiểm soát. 
Vậy thì, bạn phải làm như thế nào để chồng của bạn không phát hiện?</p>
<p>Đừng lo lắng khi gặp phải vấn đề trên, nếu bạn đang sử dụng smartphone của nhà Apple, trong bài viết này mình sẽ giới thiệu cho bạn một số cách định vị trên iPhone của chồng để theo dõi mà không sợ bị phát hiện.</p>

<h6>Định vị iphone của chồng bằng icloud</h6>
<p>Cách định vị iphone của chồng bằng icloud được xem là một cách dễ dàng nhất bởi chỉ với một vài thao tác đơn giản bạn đã có thể thực hiện cài đặt được. 
Nó không chỉ giúp  bạn tìm lại được iphone khi bị mất mà còn giúp bạn xác định được vị trí iPhone của chồng.</p>
<p>Quy trình để cài đặt định vị iphone bằng icloud như sau:</p>
<p>Bước 1: Dùng 2 thiết bị điện thoại để chia sẻ cho nhau</p>
<p>Với thiết bị của chồng , bật ứng dụng Tìm > chọn đến phần Tôi > ở đây bạn sẽ thấy mục Chia sẻ vị trí của tôi > sau đó bạn gạt Sang phải để bật vị trí. 
Tiếp theo, Chọn tab People (Người) > chọn  Start Sharing Location (Bắt đầu chia sẻ vị trí) > chọn tên trên thiết bị của bạn để thiết bị của chồng chia sẻ vị trí > chọn Send (gửi)</p>
<p>Bước 2: Tìm vị trí thiết bị của chồng</p>
<p>Trong tab People (người) , chọn thiết bị của chồng > chọn đến mục Hình thức tìm kiếm</p>
<p>Trong tab People (người) , chọn thiết bị của chồng > chọn đến mục Hình thức tìm kiếm
</p>
<p>Tiếp theo, thiết bị của bạn sẽ hiện lên 3 hình thức tìm kiếm. Chọn các hình thức mong muốn</p>

<h6>Cách định vị iphone của chồng thông qua app Find my Friends</h6>
<p>Find my Friends được tạo ra với mục đích dùng để định vị vị trí của người khác một cách bí mật. 
Chỉ cần thiết bị đã được kết nối là bạn hoàn toàn có thể dễ dàng biết được vị trí của người đó. 
Để sử dụng được ứng dụng này bạn cần truy cập vào  Appstore để tải xuống.</p>
<p>Bước 1: Sau khi đã cài đặt xong, bạn mở ứng dụng lên và chọn vào phần Tiếp theo</p>
<p>Bước 2: Nhập số điện thoại mà bạn đăng ký > chọn Tiếp theo</p>
<p>Bước 3: Nhập mật khẩu đăng nhập tài khoản > ấn chọn Tiếp theo</p>
<p>Bước 4: Đặt tên và chọn ảnh đại diện > chọn Tiếp theo</p>
<p>Bước 5: Kích vào dòng chữ “ Cho phép truy cập”</p>
<p>Bước 6: Để theo dõi chồng của mình, bạn chọn Menu biểu tượng là (dấu 3 gạch)  -> nhấn Tham gia </p>
<p>Bước 7: Nhập mã lời mời từ thiết bị bạn muốn theo dõi</p>
<p>Bước 8: Chọn Tham gia để xác nhận</p>
<h6>Cách định vị iphone của chồng thông qua Find My iPhone</h6>
<p>Find My Phone được mọi người biết đến là một trong những tính năng tìm kiếm điện thoại iPhone hàng đầu hiện nay. 
Apple đã tích hợp sẵn tính năng này trên những thiết bị điện thoại di động của họ.</p>
<p>Chúng cho phép người dùng có thể theo dõi chiếc iPhone của mình ở bất kỳ vị trí nào một cách dễ dàng, ngoài ra còn gửi được thêm lời nhắn cho người khác, xóa bỏ tất cả dữ liệu có trên thiết bị đó hay thậm chí là khóa máy.</p>
<p>Do đó, khi bạn muốn tìm cách định vị iphone của chồng, bạn có thể sử dụng tính năng hữu ích này để định vị chiếc điện thoại của chồng một cách dễ dàng, thuận tiện và nhanh chóng.</p>
<p>Bước 1: Thiết bị iPhone của chồng phải được sử dụng chung tài khoản icloud với thiết bị iphone hoặc ipad của bạn</p>
<p>Bước 2: Truy cập vào ứng dụng Find my Phone</p>
<p>Bước 3: Tìm đến mục Devices( Thiết bị) nằm trong thanh nằm ngang ở phía dưới màn hình điện thoại.</p>
<p>Bước 4: Các thiết bị iphone có dùng chung tài khoản icloud sẽ được hiển thị tại đây. Tiếp theo, bạn chọn điện thoại muốn định vị là tên  iphone của chồng</p>
<p>Bước 5: Màn hình điện thoại của bạn sẽ chuyển đến bản đồ, tại đây sẽ hiện địa điểm iPhone của chồng bạn và kèm theo các một số lựa chọn như sau:</p>


<p></p>
<p></p>
<p></p>
<p></p>
",
                    Categories =  _context.Categories.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                    Tags = _context.Tags.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                    Order = 3,
                    
                },
                // new ()
                // {
                //     Author = "buuhq",
                //     Title = "Walter White",
                //     ImageUrl = "/assets/arsha/img/team/team-1.jpg",
                //     Slug = "walter-while.html",
                //     IsPublished = true,
                //     Summary = @"Explicabo voluptatem mollitia et repellat qui dolorum quasi",
                //     Content = @"Explicabo voluptatem mollitia et repellat qui dolorum quasi",
                //     Categories =  _context.Categories.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                //     Tags = _context.Tags.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                //     Order = 1,
                //    
                // },
                // //---------------------------
                // new ()
                // {
                //     Author = "buuhq",
                //     Title = "Sarah Jhonson",
                //     ImageUrl = "/assets/arsha/img/team/team-2.jpg",
                //     Slug = "sarah-jhonson.html",
                //     IsPublished = true,
                //     Summary = @"Aut maiores voluptates amet et quis praesentium qui senda para",
                //     Content = @"Aut maiores voluptates amet et quis praesentium qui senda para",
                //     Categories =  _context.Categories.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                //     Tags = _context.Tags.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                //     Order = 2,
                //    
                // },
                // //----------------------
                // new ()
                // {
                //     Author = "buuhq",
                //     Title = "William Anderson",
                //     ImageUrl = "/assets/arsha/img/team/team-3.jpg",
                //     Slug = "william-anderson.html",
                //     IsPublished = true,
                //     Summary = @"Quisquam facilis cum velit laborum corrupti fuga rerum quia",
                //     Content = @"Quisquam facilis cum velit laborum corrupti fuga rerum quia",
                //     Categories =  _context.Categories.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                //     Tags = _context.Tags.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                //     Order = 3,
                //     
                // },
                // //----------------------
                // new ()
                // {
                //     Author = "buuhq",
                //     Title = "Amanda Jepson",
                //     ImageUrl = "/assets/arsha/img/team/team-4.jpg",
                //     Slug = "amanda-jepson.html",
                //     IsPublished = true,
                //     Summary = @"Dolorum tempora officiis odit laborum officiis et et accusamus",
                //     Content = @"Dolorum tempora officiis odit laborum officiis et et accusamus",
                //     Categories =  _context.Categories.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                //     Tags = _context.Tags.Where(x => x.Slug.Equals("tham-tu")).ToList(),
                //     Order = 4,
                //     
                // },
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
<p>Chúng tôi sẽ tư vấn cho khách hàng để có thể giải quyết và làm sáng tỏ vấn đề, tránh cho gia đình những hệ luỵ không đáng có.</p>
",
                    Content = @"
<p>Ngoại tình là nguy cơ cao nhất dẫn tới các gia đình tan rã, con cái không được sống cùng với cha mẹ. 
Những vụ án ngoại tình mà Thám tử Daily theo dõi ngoại tình cho khách hàng đã xảy ra nhiều trường hợp không thể ngờ tới. 
Có những trường hợp là nghi ngờ sai, đã được thám tử Daily điều tra và làm rõ, gia đình được gắn kết lại. 
Nhưng có những trường hợp, cha mẹ bỏ rơi con cái để đi ngoại tình hoặc nhiều đức lang quân chỉ vì chán “cơm” thèm “phở” trong phút chốc.
</p>
<p>Theo doi ngoai tinh là dịch vụ thám tử nhạy cảm và đòi hỏi nghiệp vụ thám tử tư phải sắc bén, nhanh nhạy và dấu mình tốt để không bị phát hiện. 
Chúng tôi xác thực thông tin trong thời gian theo dõi và điều tra, đưa ra những chứng cứ cụ thể và rõ ràng cho khách hàng.
</p>
<p>Nhiều cặp vợ chồng ngày nay sống rất hạnh phúc, con cái ngoan ngoãn, không thiếu thốn điều gì. 
Khi cảm thấy sự khác lạ từ đối phương, họ thường nghi ngờ và lo lắng. 
Liệu chồng hay vợ mình có ngoại tình không? Họ không ngừng nghi ngờ và dò xét, tò mò về các công việc, hoạt động bên ngoài của chồng/ vợ. 
Từ đó các mâu thuẫn xảy ra trong khi không có chứng cứ cụ thể nào. Hạnh phúc hôn nhân có nguy cơ bị tan rã, ảnh hưởng tới tinh thần của cả hai và gây ảnh hưởng cho cả con cái.
</p>
<p>Dịch vụ theo dõi ngoại tình của Thám tử Daily giúp quý khách hàng làm rõ các nghi ngờ của mình. 
Chúng tôi theo dõi chồng (theo dõi vợ ) đưa ra các bằng chứng xác thực nhất qua suốt quá trình theo doi ngoai tinh và dieu tra ngoai tinh để giải toả mối nghi ngờ của khách hàng. Đông thời tư vấn về các cách giái quyết vấn đề để làm bạn an tâm nhất.
</p>
<p>Các dấu hiệu để nghi ngờ chồng / vợ mình ngoại tình</p>
<ul>
<li>Thường xuyên nói dối</li>
<li>Lịch trình làm việc thay đổi thường xuyên hoặc sinh hoạt hàng ngày thay đổi.</li>
<li>Hình thức thay đổi: chăm chút quần áo, có hương nước hoa,…</li>
<li>Thay đổi trong sinh hoạt vợ chồng theo hướng dịu dàng hơn hoặc thô lỗ hơn; nguội lạnh trong mối quan hệ vợ chồng hoặc nồng nàn hơn bình thường.</li>
<li>Không có trách nhiệm nhiều hoặc có trách nhiệm với gia đình hơn mức bạn mong đợi, để tránh bị làm phiền hoặc trách móc nhau.</li>
<li>Hay có các cuộc điện thoại lạ hoặc không cho bạn nhấc máy</li>

</ul>

<p>Nếu bạn có bất kì nghi ngờ nào khác hoặc theo các nghi ngờ trên thì hãy liên lạc với chúng tôi qua điện thoại, đồng thời cung cấp thông tin về đối tượng. 
Chúng tôi sẽ hẹn trực tiếp gặp mặt khách hàng theo giờ quy định.
</p>
",
                   
                    Order = 1
                },
                //---------------------------
                new()
                {
                    ContentType = "Service",
                    Title = "Dịch vụ Theo dõi con cái",
                    ImageUrl = "",
                    Slug = "theo-doi-con-cai.html",
                    Summary = @"
<p>Theo dõi hoạt động hành vi con cái, giúp các bậc phụ huynh đánh giá hành vi, can thiệp hợp lý</p>
",
                    Content = @"
<p>Trong thời buổi xã hội hoá, hiện đại hoá ngày nay, các bậc cha mẹ lo làm lụng kiếm tiền mà không có thời gian để quan tâm, chăm sóc con cái. Nhiều lúc họ không biết con cái làm gì, ở đâu để có thể kiểm soát và ngăn chặn kịp thời. Họ không biết được con họ đang suy nghĩ gì và tâm sự của con thường xuyên được. 
Chính vì vậy dịch vụ thám tử giám sát học sinh – sinh viên ra đời.
</p>
<p>
Gần đây, trên phương tiện truyền thông đại chúng, trên các trang mạng xã hội thường hay đưa tin tức về các vụ mất tích bí ẩn của các em học sinh – sinh viên ở độ tuổi mới lớn, đang trong giai đoạn phát triển tâm – sinh lý. 
Các bài báo, các tin đưa tìm kiếm, cũng như các vụ bắt được kẻ tình nghi bắt cóc đang dấy lên hồi chuông báo động cho các bậc làm cha mẹ không quan tâm nhiều tới tâm lý của con. 
Thường những lo toan bận rộn trong cuộc sống hàng ngày đã khiến các bậc làm cha mẹ không còn có đủ thời gian để quan tâm con mình đang nghĩ gì, làm gì và ở đâu. 
Họ cố gắng chu cấp vật chất đầy đủ cho con cái nhưng lại không có thời gian để tìm hiểu về tâm – sinh lý của các em.
</p>

<p>
Kéo theo đó, việc các em mới bước vào giai đoạn mới lớn, trưởng thành không có sự định hướng rõ ràng, chủ yếu là theo cảm tính và nhận định riêng. Điều đó dễ khiến các em bị lạc hướng, có những suy nghĩ và hành động bị tác động bởi các yếu tố tiêu cực trong cuộc sống. Nhiều gia đình khi gặp chuyện thường nhờ tới công an để giải quyết, nhưng không phải trường hợp nào cũng có thể làm vậy. 
Với những tình huống các em học sinh – sinh viên chỉ mới phát sinh dấu hiệu mà chưa thành lập nên hành động; hoặc với các em có tính cách nóng, dễ bốc đồng thì làm vậy chỉ khiến cha mẹ không giải quyết được. 
Vì vậy, dịch vụ giám sát học sinh – sinh viên sẽ giúp các bậc cha mẹ giải quyết các vấn đề của các em. Đây là sự lựa chọn đúng đắn cho bạn khi sử dụng dịch vụ thám tử giám sát học sinh – sinh viên của thám tử Daily.
</p>
<p>
Dịch vụ giám sát học sinh – sinh viên của thám tử Minh Khang sẽ giúp quý cha mẹ:
</p>
<ul>
<li>Giám sát học sinh – sinh viên khi đang trong trường và ở ngoài trường học.</li>
<li>Theo dõi và tìm hiểu các mối quan hệ của con: biết con đang kết bạn với ai, lứa tuổi nào, tính cách và thành phần xã hội nào?</li>
<li>Theo dõi hoạt động và thói quen của con để biết con có dính vào tệ nạn nào không?</li>
</ul>

<p>Từ các thông tin này, các bậc làm cha mẹ có thể biết được tâm lý của con và có cách giải quyết thích hợp.</p>
<p>Với những dấu hiệu nào thì cha mẹ cần tới dịch vụ giám sát học sinh – sinh viên:</p>
<ul>
<li>Khi con bạn có những biểu hiện bất thường trong thói quen sinh hoạt, trong cách cư xử hoặc hành động. Và bạn không biết phải tìm hiểu con từ đâu hoặc các em không có dấu hiệu muốn chia sẻ.</li>
<li>Khi bạn phải đi công tác thường xuyên, không ở nhà nhiều để theo dõi và quản lí chuyên học hành cũng như mối quan hệ của con cái. Bạn muốn biết con cái mình đang làm gì và ở đâu? Có bị lôi kéo hoặc bị dụ dỗ hay không?</li>
<li>Khi con bạn thường xuyên về muộn hoặc viện cớ đi mà không nói thật cho gia đình biết.</li>
</ul>
<p>
Là đội ngũ thám tử có nhiều năm kinh nghiệm trong giám sát học sinh – sinh viên, nắm bát được tâm lý và dễ dàng tìm hiểu qua các phương pháp hành động không làm ảnh hưởng tới sinh hoạt của các em. 
Chúng tôi cung cấp dịch vụ giám sát học sinh – sinh viên theo ngày và trọn gói dịch vụ với giá phí phù hợp cho mỗi nhu cầu của khách hàng. 
Chúng tôi cam kết đảm bảo mọi thông tin từ khách hàng là bảo mật tuyệt đối 100%, không làm rò rỉ thông tin của khách hàng.
</p>
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
<p>
Dịch vụ thám tử điều tra huyết thống và giám định ADN của công ty thám tử tư Daily đang được rất nhiều khách hàng đánh giá cao về độ bảo mật và chính xác trong thông tin.
</p>
<p>
Hôn nhân – gia đình – huyết thống luôn là mối quan tâm lớn của gia đình và xã hội. 
Chính vì vậy, trong hệ thống dịch vụ thám tử tư tại thám tử tư Daily, bên cạnh dịch vụ theo dõi vợ/chồng ngoại tình thì nhu cầu điều tra huyết thống giám định ADN cũng là vấn đề nóng. 
Nếu không được giải quyết minh bạch, nó ảnh hưởng đến hạnh phúc gia đình, việc thừa kế cũng như một số vấn đề liên quan đến pháp lý.
</p>
<p>
Dịch vụ điều tra huyết thống giám định ADN là gì:
</p>
<p>
Giám định ADN là phân tích, so sánh những đoạn ADN tách chiết được từ tế bào của cơ thể như máu, chân tóc, mô, tinh dịch, dấu vết sinh học chứa ADN của 2 người như: Bố – con, mẹ – con, Anh/chị – em, những người trong gia định có mối quan hệ huyết thống hay không.
</p>
<p>
Lợi ích của việc điều tra huyết thống ADN:
</p>
<ul>
<li>Đa số các khách hàng đến với thám tử tư Việt Nam để xác minh huyết thống là những ông chồng nghi ngờ về cái thai trong bụng vợ, không biết rằng người cha đích thực của đứa trẻ là ai. 
Tâm lý này nếu không được giải tỏa sẽ dẫn đến những mâu thuẫn âm ỉ, dai dẳng ảnh hưởng đến hạnh phúc gia đình.</li>
<li>Xác định huyết thống chính xác các thành viên trong gia đình bị lạc, mất tích, lưu vong trong nhiều năm để người thân trong gia đình được đoàn tụ.</li>
<li>Tìm mộ người thân, điển hình là những người đã hy sinh trong chiến tranh bảo vệ tổ quốc hay nạn nhân chết do thiên tai (ví dụ như động đất), xác định mồ mả thân nhân nếu có tranh chấp.</li>

</ul>

<p>Lấy những mẫu nào để giám định ADN, xác định huyết thống và mẫu nào cho kết quả giám định chính xác nhất ?</p>
<p>Có thể tiến hành giám định ADN với nhiều loại tế bào như mẫu máu, tế bào bên niêm mạc miệng, mẫu mô, móng tay, chân tóc, cuống rốn, xương, răng vv… Các xét nghiệm sẽ có cùng độ chính xác như nhau, vì tất cả các tế bào trong cùng một cơ thể đều có cùng một loại ADN.</p>
<p>Loại mẫu nào cũng cho kết quả chính xác. Sự khác biệt ở đây giữa các mẫu khác nhau chỉ ở khâu tách chiết ADN. 
Cần có những qui trình khác nhau dùng cho các mẫu khác nhau ở khâu này.</p>
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
<p>Truy tìm chủ nhân số điện thoại
Ở thời đại 4.0 hiện nay, công nghệ đã chiếm phần lớn trong cuộc sống của con người.  
Vì vậy để tìm hiểu hay điều tra một ai đó, người ta thường nghĩ đến việc định vị qua số điện thoại.
</p>
",
                    Content = @"
<p>
Ở thời đại 4.0 hiện nay, công nghệ đã chiếm phần lớn trong cuộc sống của con người. 
Nếu xưa kia smartphone là một thứ rất xa xỉ thì bây giờ trên tay ai cũng có. 
Mọi phương thức liên lạc đều thông qua điện thoại. Thậm chí các mối quan hệ đôi khi cũng được xây dựng thông qua đây. 
Vì vậy để tìm hiểu hay điều tra một ai đó, người ta thường nghĩ đến việc định vị qua số điện thoại. 
</p>
<p>
Đương nhiên công việc này không phải ai cũng làm được, thế nên những công ty thám tử bắt đầu phát triển cả về mảng dịch vụ này. 
Nếu bạn có nhu cầu định vị, dò tìm một số thuê bao nào đó xem họ đang ở đâu thì hãy tìm đến công ty thám tử tư Daily – chuyên cung cấp dịch vụ thuê thám tử định vị số điện thoại.
</p>
<p>Định vị số điện thoại là gì?</p>
<p>
Định vị là một chức năng cho phép người dùng có thể theo dõi vị trí của một chiếc điện thoại của một người nào đó dựa trên tính năng Global Positioning System (viết tắt là GPS). 
Đây là hệ thống định vị toàn cầu, có thể định vị được vị trí của tất cả các loại điện thoại trên toàn cầu và các trạm vị trí của các nhà cung cấp dịch vụ di động. 
Thông qua số điện thoại, bạn có thể xác định được vị trí của người mà mình muốn theo dõi.
</p>
<p>Vì sao cần phải định vị số điện thoại?</p>
<p>Có khá nhiều lý do nhưng tựu chung lại có 3 lý do chính sau đây:</p>
<p>Thứ nhất là dùng để phục vụ cho việc kiểm soát số lượng điện thoại di động được phát hành mỗi năm của các nhà điều hành mạng.</p>
<p>Thứ hai là để theo dõi, giám sát vị trí hiện tại của người sử dụng số điện thoại di động đó.</p>
<p>Thứ ba để điều tra nguồn hàng, đối thủ cạnh tranh cũng như nội gián nhằm thu thập thông tin và bằng chứng.</p>
<p>Công ty thám tử tư Daily được tạo ra để giải quyết những nhu cầu này của khách hàng. 
Chúng tôi chuyện nhận những dịch vụ thuê thám tử định vị số điện thoại. 
Với tiêu chí: uy tín – chuyên nghiệp – tận tâm, chắc chắn sẽ làm vừa lòng mọi yêu cầu của khách hàng.
</p>
<p>Những trường hợp cần định vị số điện thoại: </p>
<ul>
<li>Nghi ngờ chồng/vợ mình ngoại tình</li>
<li>Con cái đi chơi về quá muộn mà không báo</li>
<li>Tìm vị trí con nợ lẩn trốn, định quỵt nợ</li>
<li>Tìm vị trí kẻ làm phiền, gây rối cho bạn</li>
<li>Tìm vị trí của bất kì ai mà bạn muốn. </li>
<li>Tìm người bỏ đi, mất tích (nếu mang điện thoại theo)</li>
</ul>
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
<p>Điều tra bản số xe là một trong những phương thức xác định thông tin của một đối tượng nào đó.</p>
",
                    Content = @"
<p>
Vì sao cần thuê thám tử điều tra biển số xe?
</p>
<p>
Có rất nhiều trường hợp cần sử dụng dịch vụ thuê thám tử điều tra biển số xe, dưới đây là một số trường hợp điển hình:
</p>
<ul>
<li>Đối tượng có hành vi mờ ám. 
Ví dụ như thường xuyên theo dõi bạn, xuất hiện với tần suất dày đặc quanh khu bạn ở. 
Đi theo bạn trên mọi đoạn đường và giữ một khoảng cách nhất định.</li>
<li>Đối tượng trốn nợ, lừa đảo tiền bạc: Có nhiều kẻ vay nợ bạn hoặc người thân họ hàng của bạn nhưng bỗng đến một ngày biến mất, tiền cũng chưa trả. 
Bạn chỉ biết rõ về xe mà họ đi, ghi nhớ cả biến số. 
Khi đó rất cần đến các thám tử để điều tra làm rõ đối tượng này.</li>
<li>Đối tượng gây tai nạn rồi bỏ trốn khỏi hiện trường: Đây là trường hợp rất cần đến các thám tử. 
Hầu hết các kẻ gây án đều hoảng loạn, lo sợ mà rời bỏ hiện trường, phóng xe đi mất. 
Vì vậy ghi nhớ được biển số xe sẽ giúp các thám tử điều tra ra thông tin của kẻ gây án.</li>
<li>Đối tượng ngoại tình với vợ/ chồng mình: Vợ/ chồng của bạn đang có những biểu hiện mờ ám, khác lạ so với ngày thường, thường xuyên có xe đưa xe đón hoặc đi lên một chiếc xe lạ. 
Rất có thể đó là xe của tình nhân. 
Vì vậy muốn biết thông tin về đối phương cách dễ dàng nhất là điều tra biển số xe. </li>
<li>Đối tượng đi cùng người thân hoặc con cái của mình: Con cái của bạn ngày càng khôn lớn, có nhiều mối giao lưu bè bạn khiến bạn không mấy yên tâm về những người mà con mình hay tiếp xúc. 
Nếu thấy ai đó khả nghi và muốn bảo vệ con gái mình nhưng không muốn làm rùm beng lên thì việc âm thầm điều tra đối phương qua biển số xe là một ý kiến không tồi.</li>
<li>Muốn thu thập thông tin về chủ xe: Đơn giản chỉ là bạn muốn biết thêm nhiều thông tin về chủ xe như họ tên, các thông tin về xe, mối quan hệ của họ với nhiều người xung quanh ra sao...</li>
<li>Điều tra tiền sử, tiền án của chủ phương tiện khi bạn nghi ngờ họ trước đây từng gây ra tai nạn gì nhưng bỏ chạy và thoát thân thành công.</li>
</ul>

<p>
Để phối hợp với các thám tử nhằm giúp công tác điều tra có kết quả nhanh chóng và chính xác nhất, khách hàng cần cung cấp chính xác biển số xe mà mình muốn điều tra. 
</p>
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
<p>Hiện trạng hàng giả, hàng nhái hiện nay Vấn nạn hàng giả, hàng nhái là một đề tài chưa bao giờ hạ nhiệt. Hệ lụy tiêu cực mà nó mang lại cho xã hội là không nhỏ...</p>
",
                    Content = @"
<p>Vấn nạn hàng giả, hàng nhái là một đề tài chưa bao giờ hạ nhiệt. 
Hệ lụy tiêu cực mà nó mang lại cho xã hội là không nhỏ như ảnh hưởng đến sức khỏe, tài chính của người tiêu dùng, làm suy giảm niềm tin của người tiêu dùng đến tính minh bạch của thị trường hàng hóa, làm giảm uy tín của các nhà sản xuất chân chính. 
</p>
<p>
Qua một số cuộc khảo sát tại nhiều tỉnh thành đặc biệt là Hà Nội và TP. HCM thì hàng giả hàng nhái xuất hiện nhan nhản khắp nơi. 
Đa phần hàng giả đều có mẫu mã đa dạng, “linh động” về giá cả và đặc biệt nguy hiểm hơn là còn phong phú cả về chủng loại. Chúng khiến người tiêu dùng không biết phân biệt đâu là thật đâu là giả. 
Chưa kể đến việc hàng giả hàng nhái còn gây ảnh hưởng nghiêm trọng đến sức khỏe con người.
</p>
<p>Vì sao nên lựa chọn công ty thám tử Thăng Long khi muốn điều tra hàng giả, hàng nhái?</p>
<ul>
<li>Công ty thám tử Daily có trụ sở cả ở Hà Nội và TP Hồ Chí Minh – hai địa điểm có số lượng hàng giả, hàng kém chất lượng xuất hiện nhiều nhất. 
Vì vậy nếu có nhu cầu, quý khách hàng có thể dễ dàng liên hệ với chúng tôi.</li>
<li>Đội ngũ tại công ty là những con người dày dạn kinh nghiệm, làm việc với một thái độ chuyên nghiệp, nhiệt tình. 
Phần lớn đều xuất thân từ công an về hưu, bộ đội xuất ngũ được đào tạo bài bản có trình độ chuyên môn cao đáp ứng mọi yêu cầu thông tin mà khách hàng cần.</li>
<li>Vì hàng giả, hàng nhái hiện nay được làm với mức độ tinh vi, mắt thường cũng khó có thể phát hiện nhưng đừng lo, công ty chúng tôi luôn trang bị đầy đủ các thiết bị hiện đại, tối tân nhất để phục vụ quá trình điều tra diễn ra nhanh chóng, chính xác.</li>
<li></li>
<li></li>
<li></li>
<li></li>
<li></li>
</ul>

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
<p>Thám tử tư Daily cung cấp dịch vụ điều tra trộm cắp doanh nghiệp uy tín, cam kết nhanh chóng tìm ra thủ phạm trộm cắp cùng đầy đủ chứng cứ xác thực.</p>
",
                    Content = @"
<p>Thám tử tư Daily cung cấp dịch vụ điều tra trộm cắp doanh nghiệp uy tín, cam kết nhanh chóng tìm ra thủ phạm trộm cắp cùng đầy đủ chứng cứ xác thực. 
Với thâm niêm hàng chục năm trong nghề, chúng tôi có đội ngũ thám tử giỏi cùng các thiết bị theo dõi giám sát hiện đại sẽ mang lại kết quả tốt nhất cho khách hàng.</p>
<p>Như chúng ta đã biết, tại Việt Nam có hàng trăm nghìn doanh nghiệp khác nhau, mỗi lĩnh vực sẽ có hàng loạt đối thủ cạnh tranh. 
Chính vì vậy mà rất dễ xảy ra tình trạng bị ăn cắp bản quyền hoặc bị mất cắp tài sản, dữ liệu quan trọng của công ty.</p>
<p>Hơn nữa kẻ trộm đây rất có thể là do chính người trong công ty, nội bộ công ty làm ra. 
Có thể do lòng tham, bị mua chuộc hoặc bất mãn trong nội bộ công ty nên đã có hành vi ăn trộm, gây ra những thiệt hại lớn cho công ty của bạn. 
Thậm chí có những dữ liệu quan trọng một khi bị rò rỉ có thể khiến công ty bạn phá sản.</p>
<p>Thêm vào đó, bạn lại nghi ngờ thông tin, tài sản bị đánh cắp đó lại do chính người công ty làm ra. 
Nhưng bạn lại không có bằng chứng xác thực, bạn chưa chắc chắn.</p>
<p>Với chuyên môn và kinh nghiệm, các thám tử sẽ bí mật điều tra, làm rõ ai là người trộm cắp, thông tin cá nhân của họ, mục đích họ làm thế để làm gì…Qua đó có biện pháp giải quyết kịp thời, đồng thời cũng răn đe để tránh xảy ra tình trạng tương tự....</p>

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
<p>Bạn đang muốn điều tra đối thủ cạnh tranh của mình để tìm hiểu về họ, quan hệ đối tác hoặc bí mật kinh doanh của họ. 
Điều này giúp bạn cạnh tranh hơn và thu được nhiều lợi ích trên thị trường. 
Sau đây là những thông tin về dịch vụ điều tra đối thủ cạnh tranh , cũng như giới thiệu đến bạn công ty thám tử tư Daily uy tín hàng đầu hiện nay, để giúp bạn giải quyết vấn đề nhanh nhất.</p>
<h5>Dịch vụ điều tra đối thủ cạnh tranh là gì?</h5>
<p>Điều tra đối thủ cạnh tranh là dịch vụ thám tử thu thập thông tin liên quan đến các vấn đề chiến lược, phạm vi cạnh tranh, mục tiêu kinh doanh thị trường, lên kế hoạch chiến lược và các vấn đề nội bộ.</p>
<p>Thị trường kinh doanh ngày nay vô cùng màu mỡ, điều tra đối thủ cạnh tranh đôi khi quyết định sự thành bại của một doanh nghiệp, chứa đựng nhiều tiềm năng nhưng cũng không tránh khỏi những cạm bẫy. Khi nắm vững được chiến lược phát triển đối thủ cạnh tranh, doanh nghiệp có cơ hội thăng tiến cao hơn.</p>

",
                    Order = 8,

                },
            };
            await _context.SiteContents.AddRangeAsync(siteContents);
        }
    }
}
