using ECommerceSystem.Data;
using ECommerceSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace ECommerceSystem
{
    internal class Program
    {
        // إنشاء كائن من قاعدة البيانات للتعامل مع EF Core
        static  ApplicationDbContext context = new ApplicationDbContext();


        //******************************************* 1- Register a New User ***************************************//
        static void RegisterUser(ApplicationDbContext context)
        {
            // عنوان العملية
            Console.WriteLine("===== Register New User =====");

            {
                Console.Write("Username: ");
                string username = Console.ReadLine();

                Console.Write("Full Name: ");
                string fullName = Console.ReadLine();

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("Password: ");
                string password = Console.ReadLine();

                Console.Write("Phone Number (optional): ");
                string? phone = Console.ReadLine();

                Console.Write("Address (optional): ");
                string? address = Console.ReadLine();

                User user = new User
                {
                    Username = username,
                    FullName = fullName,
                    Email = email,
                    PasswordHash = password,
                    PhoneNumber = phone,
                    Address = address,
                    RegistrationDate = DateTime.Now,
                    IsActive = true
                };

                context.Users.Add(user);
                context.SaveChanges();

                Console.WriteLine($"User Registered Successfully.");
                Console.WriteLine($"Assigned User ID = {user.UserId}");
            }
        }
        //******************************************** 2- Add a New Product to a Category **************************//
        static void AddProduct(ApplicationDbContext context)
        {
            Console.WriteLine("===== Add Product =====");

            Console.WriteLine("Categories:");

            foreach (var c in context.Categories.ToList())
            {
                Console.WriteLine($"{c.CategoryId} - {c.CategoryName}");
            }

            Console.Write("Category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            Console.Write("Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Stock: ");
            int stock = int.Parse(Console.ReadLine());

            Console.Write("Image Url: ");
            string image = Console.ReadLine();

            Product product = new Product
            {
                ProductName = name,
                Description = description,
                Price = price,
                StockQuantity = stock,
                ImageUrl = image,
                CategoryId = categoryId,
                CreatedAt = DateTime.Now,
                IsAvailable = true
            };

            var category = context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            context.Products.Add(product);

            context.SaveChanges();

            Console.WriteLine("Product Added.");
        }
        //******************************************** 3- Place an Order ******************************************//
        static void PlaceOrder(ApplicationDbContext context)
        {
            Console.WriteLine("===== Write Plac eOrder =====");

            // 1- عرض المستخدمين

            Console.WriteLine("Available Users:");

            var users = context.Users.ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.UserId} - {user.FullName}");
            }

            // 2- اختيار المستخدم

            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());

            Console.Write("Enter Shipping Address: ");
            string shippingAddress = Console.ReadLine();

            Console.Write("Enter Payment Method (Cash / Card): ");
            string paymentMethod = Console.ReadLine();

            // 3- إنشاء الطلب
            Order order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = 0,
                Status = "Pending",
                ShippingAddress = shippingAddress,
                PaymentMethod = paymentMethod
            };

            context.Orders.Add(order);
            context.SaveChanges();

            Console.WriteLine($"Order Created Successfully. Order ID = {order.OrderId}");
        }

        //******************************************** 4-  Write Product Review ***********************************//
        static void WriteProductReview(ApplicationDbContext context)
        {

            Console.WriteLine("===== Write Product Review =====");

            //اعرض المستخدمين
            Console.WriteLine("\nAvailable Users:");

            var users = context.Users.ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.UserId} - {user.FullName}");
            }

            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());


            //اعرض المنتجات

            Console.WriteLine("\nAvailable Products:");

            var products = context.Products.ToList();

            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductId} - {product.ProductName}");
            }

            Console.Write("Enter Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            //اكتب التقييم والتعليق
            Console.Write("Rating (1-5): ");
            int rating = int.Parse(Console.ReadLine());

            Console.Write("Comment: ");
            string comment = Console.ReadLine();

            // إنشاء المراجعة
            Review review = new Review
            {
                UserId = userId,
                ProductId = productId,
                Rating = rating,
                Comment = comment,
                ReviewDate = DateTime.Now
            };

            var selectedUser = context.Users.FirstOrDefault(u => u.UserId == userId);

            if (selectedUser == null)
            {
                Console.WriteLine("User not found!");
                return;
            }

            var selectedProduct = context.Products.FirstOrDefault(p => p.ProductId == productId);

            if (selectedProduct == null)
            {
                Console.WriteLine("Product not found!");
                return;
            }

            context.Reviews.Add(review);
            context.SaveChanges();

            Console.WriteLine("\nReview added successfully.");
        }
        //******************************************** 5- Write Product Review ************************************//
        static void UpdateProduct(ApplicationDbContext context)
        {
            Console.WriteLine("===== Update Product Price and Availability =====");

            Console.Write("Enter Product ID: ");
            int id = int.Parse(Console.ReadLine());

            // البحث عن المنتج
            var product = context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product != null)
            {
                // تحديث السعر
                Console.Write("Enter new price: ");
                product.Price = decimal.Parse(Console.ReadLine());

                // تحديث حالة التوفر
                Console.Write("Is Available (true/false): ");
                product.IsAvailable = bool.Parse(Console.ReadLine());

                // حفظ التغييرات في قاعدة البيانات
                context.SaveChanges();

                // رسالة تأكيد
                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }

        }

        //******************************************** 6- Cancel Order ********************************************//
        static void CancelOrder(ApplicationDbContext context)
        {
            Console.WriteLine("===== Cancel Order =====");

            Console.Write("Enter Order ID: ");
            int orderId = int.Parse(Console.ReadLine());

            // البحث عن الطلب
            var order = context.Orders.FirstOrDefault(o => o.OrderId == orderId);

            if (order != null)
            {
                // جلب جميع عناصر الطلب
                var orderItems = context.OrderItems
                                        .Where(oi => oi.OrderId == orderId)
                                        .ToList();

                // استرجاع الكمية لكل منتج
                foreach (var item in orderItems)
                {
                    var product = context.Products.FirstOrDefault(p => p.ProductId == item.ProductId);

                    if (product != null)
                    {
                        product.StockQuantity += item.Quantity;
                    }
                }

                // تغيير حالة الطلب
                order.Status = "Cancelled";

                // حفظ التغييرات
                context.SaveChanges();

                Console.WriteLine("Order cancelled successfully.");
            }
            else
            {
                Console.WriteLine("Order not found.");
            }
        }

        //******************************************** 7- Cancel Order *******************************************//
        static void DeleteReview(ApplicationDbContext context)
        {
            Console.WriteLine("===== Delete a Review =====");

            Console.Write("Enter Review ID: ");
            int reviewId = int.Parse(Console.ReadLine());

            // البحث عن المراجعة
            var review = context.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);

            if (review != null)
            {
                // حذف المراجعة
                context.Reviews.Remove(review);

                // حفظ التغييرات
                context.SaveChanges();

                Console.WriteLine("Review deleted successfully.");
            }
            else
            {
                Console.WriteLine("Review not found.");
            }

        }

        //******************************************** 8- View All Products ******************************************//
        static void ViewAllProducts(ApplicationDbContext context)
        {
            Console.WriteLine("===== View All Products =====");


            // جلب جميع المنتجات
            var products = context.Products.ToList();

            if (products.Any())
            {
                foreach (var product in products)
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Product ID: {product.ProductId}");
                    Console.WriteLine($"Name: {product.ProductName}");
                    Console.WriteLine($"Price: {product.Price}");
                    Console.WriteLine($"Stock Quantity: {product.StockQuantity}");
                    Console.WriteLine($"Available: {product.IsAvailable}");
                }
            }
            else
            {
                Console.WriteLine("No products found.");
            }
        }

        //***************************************** MENU ******************************************************//



        static void Main(string[] args)
        {
            using var context = new ApplicationDbContext();

            while (true)
            {
                Console.WriteLine("\n===== E-Commerce System =====");
                Console.WriteLine("1. Register User");
                Console.WriteLine("2. Add Product");
                Console.WriteLine("3. Place Order");
                Console.WriteLine("4. Write Product Review");
                Console.WriteLine("5. Update Product");
                Console.WriteLine("6. Cancel Order");
                Console.WriteLine("7. Delete Review");
                Console.WriteLine("8. View All Products");
                Console.WriteLine("9. Filter Products");
                Console.WriteLine("10. Category With Products");
                Console.WriteLine("11. Order History");
                Console.WriteLine("12. Product Summary");
                Console.WriteLine("0. Exit");

                Console.Write("Choose: ");
                
                
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1: RegisterUser(context); break;
                    case 2: AddProduct(context); break;
                    case 3: PlaceOrder(context); break;
                    case 4: WriteProductReview(context); break;
                    case 5: UpdateProduct(context); break;
                    case 6: CancelOrder(context); break;
                    case 7: DeleteReview(context); break;
                    case 8: ViewAllProducts(context); break;
                    //case 9: FilterProducts(context); break;
                    //case 10: GetCategoryWithProducts(context); break;
                    //case 11: ViewOrderHistory(context); break;
                    //case 12: ProductSummaryReport(context); break;
                    case 0: return;
                }
            }
        }
    }
}
        
        
