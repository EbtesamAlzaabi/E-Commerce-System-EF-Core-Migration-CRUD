using ECommerceSystem.Data;
using ECommerceSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace ECommerceSystem
{
    internal class Program
    {
        // إنشاء كائن من قاعدة البيانات للتعامل مع EF Core
        static ApplicationDbContext context = new ApplicationDbContext();

        public static void RegisterNewUser()
        {
            // عنوان العملية
            Console.WriteLine("===== Register New User =====");

            // إنشاء كائن جديد من User
            User user = new User();

            // قراءة اسم المستخدم من المستخدم
            Console.Write("Username: ");
            user.Username = Console.ReadLine();

            // قراءة الاسم الكامل
            Console.Write("Full Name: ");
            user.FullName = Console.ReadLine();

            // قراءة البريد الإلكتروني
            Console.Write("Email: ");
            user.Email = Console.ReadLine();

            // قراءة كلمة المرور (سيتم تخزينها في PasswordHash حسب تصميم المشروع)
            Console.Write("Password: ");
            user.PasswordHash = Console.ReadLine();

            // قراءة رقم الهاتف
            Console.Write("Phone Number: ");
            user.PhoneNumber = Console.ReadLine();

            // قراءة العنوان
            Console.Write("Address: ");
            user.Address = Console.ReadLine();

            // تعيين تاريخ التسجيل إلى الوقت الحالي
            user.RegistrationDate = DateTime.Now;

            // جعل المستخدم نشطًا بشكل افتراضي
            user.IsActive = true;

            // إضافة المستخدم إلى جدول Users داخل EF Core
            context.Users.Add(user);

            // حفظ البيانات في قاعدة البيانات
            context.SaveChanges();

            // عرض رقم المستخدم الذي أنشأته قاعدة البيانات تلقائيًا
            Console.WriteLine($"User Registered Successfully. ID = {user.UserId}");
        }


        public static void AddNewProduct()
        {
            // عرض عنوان العملية
            Console.WriteLine("===== Categories =====");

            // جلب جميع التصنيفات من قاعدة البيانات وعرضها للمستخدم
            foreach (var c in context.Categories.ToList())
            {
                Console.WriteLine($"{c.CategoryId} - {c.CategoryName}");
            }

            // إنشاء كائن جديد من Product
            Product product = new Product();

            // قراءة رقم التصنيف الذي اختاره المستخدم
            Console.Write("Category ID: ");
            product.CategoryId = int.Parse(Console.ReadLine());

            // قراءة اسم المنتج
            Console.Write("Product Name: ");
            product.ProductName = Console.ReadLine();

            // قراءة وصف المنتج
            Console.Write("Description: ");
            product.Description = Console.ReadLine();

            // قراءة سعر المنتج
            Console.Write("Price: ");
            product.Price = decimal.Parse(Console.ReadLine());

            // قراءة كمية المنتج الموجودة في المخزون
            Console.Write("Stock Quantity: ");
            product.StockQuantity = int.Parse(Console.ReadLine());

            // قراءة رابط صورة المنتج (اختياري)
            Console.Write("Image URL: ");
            product.ImageUrl = Console.ReadLine();

            // تعيين تاريخ إنشاء المنتج إلى الوقت الحالي
            product.CreatedAt = DateTime.Now;

            // جعل المنتج متاحًا للبيع بشكل افتراضي
            product.IsAvailable = true;

            // إضافة المنتج إلى جدول Products
            context.Products.Add(product);

            // حفظ البيانات في قاعدة البيانات
            context.SaveChanges();

            // عرض رسالة نجاح
            Console.WriteLine("Product Added Successfully.");
        }



        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n===== E-Commerce System =====");
                Console.WriteLine("1. Register User");
                Console.WriteLine("2. Add Product");

                Console.Write("Choose: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        RegisterNewUser();
                        break;

                    case 2:
                        AddNewProduct();
                        break;

                    case 0:
                        return;
                }
            }
        }
    }
}
        
        
