using EcisApi.Helpers;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            // Look for any roles.
            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }

            // SystemConfiguration seeding
            SystemConfiguration systemConfiguration1 = new()
            {
                ConfigurationKey = ConfigurationKeys.MODIFICATION_VALID_DURATION,
                ConfigurationValue = "1-year",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false
            };
            context.SystemConfigurations.Add(systemConfiguration1);

            // Role seeding
            Role adminRole = new() { 
                RoleName = "Admin",
                CreatedAt = DateTime.Now,
                Description = "Admin Role",
                IsDeleted = false,
                UpdatedAt = DateTime.Now,
                HasManagement = true
            };
            context.Roles.Add(adminRole);

            Role agentRole = new()
            {
                RoleName = "Agent",
                CreatedAt = DateTime.Now,
                Description = "Agent Role",
                IsDeleted = false,
                UpdatedAt = DateTime.Now,
                HasManagement = true
            };
            context.Roles.Add(agentRole);

            Role companyRole = new()
            {
                RoleName = "Company",
                CreatedAt = DateTime.Now,
                Description = "Company Role",
                IsDeleted = false,
                UpdatedAt = DateTime.Now,
                HasManagement = false
            };
            context.Roles.Add(companyRole);

            Role thirdPartyRole = new()
            {
                RoleName = "ThirdParty",
                CreatedAt = DateTime.Now,
                Description = "Third Party Role",
                IsDeleted = false,
                UpdatedAt = DateTime.Now,
                HasManagement = false
            };
            context.Roles.Add(thirdPartyRole);

            Role superUserRole = new()
            {
                RoleName = "SuperUser",
                CreatedAt = DateTime.Now,
                Description = "Super User Role",
                IsDeleted = false,
                UpdatedAt = DateTime.Now,
                HasManagement = true
            };
            context.Roles.Add(superUserRole);


            // CriteriaType seeding
            CriteriaType criteriaType1 = new()
            {
                CriteriaTypeName = "Tuân thủ quy định của pháp luật trong việc thành lập và hoạt động của doanh nghiệp",
                Description = "Tuân thủ quy định của pháp luật trong việc thành lập và hoạt động của doanh nghiệp",
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.CriteriaTypes.Add(criteriaType1);

            CriteriaType criteriaType2 = new()
            {
                CriteriaTypeName = "Tuân thủ quy định của pháp luật về nguồn gốc gỗ hợp pháp",
                Description = "Tuân thủ quy định của pháp luật về nguồn gốc gỗ hợp pháp",
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.CriteriaTypes.Add(criteriaType2);
            context.SaveChanges();

            // Criteria seeding
            Criteria criteria1 = new()
            {
                CriteriaName = "Thành lập doanh nghiệp",
                Description = "Tuân thủ quy định của pháp luật về thành lập doanh nghiệp phải có các loại tài liệu sau:",
                CriteriaTypeId = criteriaType1.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.Criterias.Add(criteria1);

            Criteria criteria2 = new()
            {
                CriteriaName = "Môi trường",
                Description = "Tuân thủ quy định của pháp luật về môi trường phải có một trong các loại tài liệu sau:",
                CriteriaTypeId = criteriaType1.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.Criterias.Add(criteria2);

            Criteria criteria3 = new()
            {
                CriteriaName = "Phòng cháy, chữa cháy",
                Description = "Tuân thủ quy định của pháp luật về phòng cháy, chữa cháy phải có một trong các loại tài liệu sau:",
                CriteriaTypeId = criteriaType1.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.Criterias.Add(criteria3);

            Criteria criteria4 = new()
            {
                CriteriaName = "Theo dõi nhập, xuất lâm sản",
                Description = "Tuân thủ quy định của pháp luật về theo dõi nhập, xuất lâm sản phải có một trong các loại tài liệu sau:",
                CriteriaTypeId = criteriaType1.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.Criterias.Add(criteria4);

            Criteria criteria5 = new()
            {
                CriteriaName = "Thuế, lao động",
                Description = "Tuân thủ quy định của pháp luật về thuế, lao động phải có một trong các loại tài liệu sau:",
                CriteriaTypeId = criteriaType1.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.Criterias.Add(criteria5);

            Criteria criteria6 = new()
            {
                CriteriaName = "Hồ sơ khai thác gỗ đối với doanh nghiệp chế biến và xuất khẩu gỗ trực tiếp khai thác gỗ làm nguyên liệu chế biến",
                Description = "Tuân thủ quy định của pháp luật về hồ sơ khai thác gỗ đối với doanh nghiệp chế biến và xuất khẩu gỗ trực tiếp khai thác gỗ làm nguyên liệu chế biến",
                CriteriaTypeId = criteriaType2.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.Criterias.Add(criteria6);

            Criteria criteria7 = new()
            {
                CriteriaName = "Hồ sơ sau xử lý tịch thu đối với doanh nghiệp chế biến và xuất khẩu gỗ sử dụng gỗ sau tịch thu làm nguyên liệu chế biến",
                Description = "Tuân thủ quy định của pháp luật về hồ sơ sau xử lý tịch thu đối với doanh nghiệp chế biến và xuất khẩu gỗ sử dụng gỗ sau tịch thu làm nguyên liệu chế biến",
                CriteriaTypeId = criteriaType2.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.Criterias.Add(criteria7);

            Criteria criteria8 = new()
            {
                CriteriaName = "Hồ sơ gỗ nhập khẩu đối với doanh nghiệp chế biến và xuất khẩu gỗ sử dụng gỗ nhập khẩu làm nguyên liệu chế biến",
                Description = "Tuân thủ quy định của pháp luật về hồ sơ gỗ nhập khẩu đối với doanh nghiệp chế biến và xuất khẩu gỗ sử dụng gỗ nhập khẩu làm nguyên liệu chế biến",
                CriteriaTypeId = criteriaType2.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.Criterias.Add(criteria8);

            Criteria criteria9 = new()
            {
                CriteriaName = "Hồ sơ trong quá trình mua bán, vận chuyển; chế biến",
                Description = "Tuân thủ quy định của pháp luật về hồ sơ trong quá trình mua bán, vận chuyển; chế biến",
                CriteriaTypeId = criteriaType2.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.Criterias.Add(criteria9);
            context.SaveChanges();

            // CriteriaDetail seeding
            CriteriaDetail criteriaDetail1 = new()
            {
                CriteriaDetailName = "Giấy chứng nhận đăng ký doanh nghiệp",
                Description = "Giấy chứng nhận đăng ký doanh nghiệp",
                CriteriaId = criteria1.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = true
            };
            context.CriteriaDetails.Add(criteriaDetail1);

            CriteriaDetail criteriaDetail2 = new()
            {
                CriteriaDetailName = "Giấy chứng nhận đăng ký đầu tư đối với doanh nghiệp có vốn đầu tư nước ngoài hoặc có yếu tố nước ngoài chiếm 51% vốn điều lệ hoặc doanh nghiệp hoạt động trong khu công nghiệp, khu chế xuất",
                Description = "Giấy chứng nhận đăng ký đầu tư đối với doanh nghiệp có vốn đầu tư nước ngoài hoặc có yếu tố nước ngoài chiếm 51% vốn điều lệ hoặc doanh nghiệp hoạt động trong khu công nghiệp, khu chế xuất",
                CriteriaId = criteria1.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = false
            };
            context.CriteriaDetails.Add(criteriaDetail2);

            CriteriaDetail criteriaDetail3 = new()
            {
                CriteriaDetailName = "Quyết định phê duyệt báo cáo đánh giá tác động môi trường đối với cơ sở chế biến gỗ, dăm gỗ từ gỗ rừng tự nhiên có công suất từ 5.000 m3 sản phẩm/năm trở lên",
                Description = "Quyết định phê duyệt báo cáo đánh giá tác động môi trường đối với cơ sở chế biến gỗ, dăm gỗ từ gỗ rừng tự nhiên có công suất từ 5.000 m3 sản phẩm/năm trở lên",
                CriteriaId = criteria2.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = false,
            };
            context.CriteriaDetails.Add(criteriaDetail3);

            CriteriaDetail criteriaDetail4 = new()
            {
                CriteriaDetailName = "Quyết định phê duyệt báo cáo đánh giá tác động môi trường đối với cơ sở sản xuất ván ép có công suất từ 100.000 m2 sản phẩm/năm trở lên",
                Description = "Quyết định phê duyệt báo cáo đánh giá tác động môi trường đối với cơ sở sản xuất ván ép có công suất từ 100.000 m2 sản phẩm/năm trở lên",
                CriteriaId = criteria2.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = false
            };
            context.CriteriaDetails.Add(criteriaDetail4);

            CriteriaDetail criteriaDetail5 = new()
            {
                CriteriaDetailName = "Quyết định phê duyệt báo cáo đánh giá tác động môi trường đối với cơ sở sản xuất đồ gỗ có tổng diện tích kho bãi, nhà xưởng từ 10.000 m2 trở lên",
                Description = "Quyết định phê duyệt báo cáo đánh giá tác động môi trường đối với cơ sở sản xuất đồ gỗ có tổng diện tích kho bãi, nhà xưởng từ 10.000 m2 trở lên",
                CriteriaId = criteria2.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = false
            };
            context.CriteriaDetails.Add(criteriaDetail5);

            CriteriaDetail criteriaDetail6 = new()
            {
                CriteriaDetailName = "Có kế hoạch bảo vệ môi trường đối với các cơ sở sản xuất có công suất hay diện tích nhỏ hơn công suất hoặc diện tích của các cơ sở sản xuất quy định tại các điểm a, b, c nêu trên",
                Description = "Có kế hoạch bảo vệ môi trường đối với các cơ sở sản xuất có công suất hay diện tích nhỏ hơn công suất hoặc diện tích của các cơ sở sản xuất quy định tại các điểm a, b, c nêu trên",
                CriteriaId = criteria2.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = false
            };
            context.CriteriaDetails.Add(criteriaDetail6);

            CriteriaDetail criteriaDetail7 = new()
            {
                CriteriaDetailName = "Phương án phòng cháy, chữa cháy theo quy định pháp luật",
                Description = "Phương án phòng cháy, chữa cháy theo quy định pháp luật",
                CriteriaId = criteria3.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = true
            };
            context.CriteriaDetails.Add(criteriaDetail7);

            CriteriaDetail criteriaDetail8 = new()
            {
                CriteriaDetailName = "Sổ theo dõi nhập, xuất lâm sản được ghi chép đầy đủ theo đúng quy định pháp luật",
                Description = "Sổ theo dõi nhập, xuất lâm sản được ghi chép đầy đủ theo đúng quy định pháp luật",
                CriteriaId = criteria4.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = true
            };
            context.CriteriaDetails.Add(criteriaDetail8);

            CriteriaDetail criteriaDetail9 = new()
            {
                CriteriaDetailName = "Không có tên trong danh sách công khai thông tin tổ chức, cá nhân kinh doanh có dấu hiệu vi phạm pháp luật về thuế",
                Description = "Không có tên trong danh sách công khai thông tin tổ chức, cá nhân kinh doanh có dấu hiệu vi phạm pháp luật về thuế",
                CriteriaId = criteria5.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = true
            };
            context.CriteriaDetails.Add(criteriaDetail9);

            CriteriaDetail criteriaDetail10 = new()
            {
                CriteriaDetailName = "Có kế hoạch vệ sinh an toàn lao động theo quy định của pháp luật",
                Description = "Có kế hoạch vệ sinh an toàn lao động theo quy định của pháp luật",
                CriteriaId = criteria5.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = true
            };
            context.CriteriaDetails.Add(criteriaDetail10);

            CriteriaDetail criteriaDetail11 = new()
            {
                CriteriaDetailName = "Người lao động có tên trong danh sách bảng lương của doanh nghiệp",
                Description = "Người lao động có tên trong danh sách bảng lương của doanh nghiệp",
                CriteriaId = criteria5.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = true
            };
            context.CriteriaDetails.Add(criteriaDetail11);

            CriteriaDetail criteriaDetail12= new()
            {
                CriteriaDetailName = "Niêm yết công khai thông tin về đóng bảo hiểm xã hội và y tế đối với người lao động theo quy định của Luật Bảo hiểm xã hội",
                Description = "Niêm yết công khai thông tin về đóng bảo hiểm xã hội và y tế đối với người lao động theo quy định của Luật Bảo hiểm xã hội",
                CriteriaId = criteria5.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = true
            };
            context.CriteriaDetails.Add(criteriaDetail12);

            CriteriaDetail criteriaDetail13 = new()
            {
                CriteriaDetailName = "Người lao động là thành viên tổ chức Công đoàn của doanh nghiệp",
                Description = "Người lao động là thành viên tổ chức Công đoàn của doanh nghiệp",
                CriteriaId = criteria5.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = true
            };
            context.CriteriaDetails.Add(criteriaDetail13);

            CriteriaDetail criteriaDetail14 = new()
            {
                CriteriaDetailName = "Chấp hành quy định về trình tự, thủ tục khai thác gỗ",
                Description = "Chấp hành quy định về trình tự, thủ tục khai thác gỗ",
                CriteriaId = criteria6.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = false
            };
            context.CriteriaDetails.Add(criteriaDetail14);

            CriteriaDetail criteriaDetail15 = new()
            {
                CriteriaDetailName = "Bảng kê gỗ theo quy định của pháp luật",
                Description = "Bảng kê gỗ theo quy định của pháp luật",
                CriteriaId = criteria6.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = false
            };
            context.CriteriaDetails.Add(criteriaDetail15);

            CriteriaDetail criteriaDetail16 = new()
            {
                CriteriaDetailName = "Bản sao hồ sơ nguồn gốc gỗ khai thác",
                Description = "Bản sao hồ sơ nguồn gốc gỗ khai thác",
                CriteriaId = criteria6.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = false
            };
            context.CriteriaDetails.Add(criteriaDetail16);

            CriteriaDetail criteriaDetail17 = new()
            {
                CriteriaDetailName = "Bảng kê gỗ theo quy định của pháp luật",
                Description = "Bảng kê gỗ theo quy định của pháp luật",
                CriteriaId = criteria7.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = false
            };
            context.CriteriaDetails.Add(criteriaDetail17);

            CriteriaDetail criteriaDetail18 = new()
            {
                CriteriaDetailName = "Bản sao hồ sơ gỗ sau xử lý tịch thu",
                Description = "Bản sao hồ sơ gỗ sau xử lý tịch thu",
                CriteriaId = criteria7.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = false
            };
            context.CriteriaDetails.Add(criteriaDetail18);

            CriteriaDetail criteriaDetail19 = new()
            {
                CriteriaDetailName = "Bảng kê gỗ theo quy định của pháp luật",
                Description = "Bảng kê gỗ theo quy định của pháp luật",
                CriteriaId = criteria8.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = false
            };
            context.CriteriaDetails.Add(criteriaDetail19);

            CriteriaDetail criteriaDetail20 = new()
            {
                CriteriaDetailName = "Bản sao hồ sơ gỗ nhập khẩu",
                Description = "Bản sao hồ sơ gỗ nhập khẩu",
                CriteriaId = criteria8.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = false
            };
            context.CriteriaDetails.Add(criteriaDetail20);

            CriteriaDetail criteriaDetail21 = new()
            {
                CriteriaDetailName = "Bảng kê gỗ theo quy định của pháp luật",
                Description = "Bảng kê gỗ theo quy định của pháp luật",
                CriteriaId = criteria9.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = false
            };
            context.CriteriaDetails.Add(criteriaDetail21);

            CriteriaDetail criteriaDetail22 = new()
            {
                CriteriaDetailName = "Bản sao hồ sơ nguồn gốc gỗ",
                Description = "Bản sao hồ sơ nguồn gốc gỗ",
                CriteriaId = criteria9.Id,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRequired = false
            };
            context.CriteriaDetails.Add(criteriaDetail22);

            //context.Accounts.Add(new Account
            //{
            //    Email = "qanh@gmail.com",
            //    Password = CommonUtils.GenerateSHA1("abcd1234"),
            //    Role = adminRole,
            //    IsDeleted = false,
            //    CreatedAt = DateTime.Now,
            //    UpdatedAt = DateTime.Now,
            //    IsVerified = true
            //});

            context.SaveChanges();

            //CompanyType seeding
            CompanyType companyType1 = new()
            {
                TypeName = "Loại 1",
                Description = "Doanh nghiệp loại 1",
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.CompanyTypes.Add(companyType1);

            CompanyType companyType2 = new()
            {
                TypeName = "Loại 2",
                Description = "Doanh nghiệp loại 2",
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.CompanyTypes.Add(companyType2);

            context.SaveChanges();
        }
    }
}
