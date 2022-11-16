using Product;
using Product.Context;
using Microsoft.EntityFrameworkCore;
using Product.Interface;
using Product.Models;
using Product.Services;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            IServiceCollection services = builder.Services;
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            ConfigureServices(services, connectionString);
            var app = builder.Build();

            app.Map("/searchform1", SearchForm1ForGenres);
            app.Map("/searchform2", SearchForm2ForGenres);
            app.Map("/ActivityTypes", TableActivityTypes);
            app.Map("/Companies", TableCompanies);
            app.Map("/MeasurementUnits", TableMeasurementUnits);
            app.Map("/OwnershipForms", TableOwnershipForms);
            app.Map("/ProductionTypes", TableProductionTypes);
            app.Map("/ProductReleasePlans", TableProductReleasePlans);
            app.Map("/Products", TableProducts);
            app.Map("/ProductSalesPlans", TableProductSalesPlans);
            app.Map("/info", Info);
            app.Run((context) =>
            {
                string responseString = "<HTML><TITLE>Главная</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>";
                responseString += "<UL>";
                responseString += "<LI><A href='/'>Главная</A></LI>";
                responseString += "<LI><A href='/ActivityTypes'>Вид деятельности</A></LI>";
                responseString += "<LI><A href='/Companies'>Компании</A></LI>";
                responseString += "<LI><A href='/MeasurementUnits'>Единицы измерения</A></LI>";
                responseString += "<LI><A href='/OwnershipForms'>Формы собственности</A></LI>";
                responseString += "<LI><A href='/ProductionTypes'>Типы продукции</A></LI>";
                responseString += "<LI><A href='/ProductReleasePlans'>Планы выпуска продукции</A></LI>";
                responseString += "<LI><A href='/Products'>Продукты</A></LI>";
                responseString += "<LI><A href='/ProductSalesPlans'>Планы продаж продукции</A></LI>";
                responseString += "<LI><A href='/searchform1'>Форма #1 для таблицы виды деятельности</A></LI>";
                responseString += "<LI><A href='/searchform2'>Форма #2 для таблицы виды деятельности</A></LI>";
                responseString += "<LI><A href='/info'>Информация о клиенте</A></LI>";
                responseString += "</UL></BODY></HTML>";
                return context.Response.WriteAsync(responseString);
            });

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProductContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<GenericCachedClassService<ActivityTypes>>();
            services.AddTransient<GenericCachedClassService<Companies>>();
            services.AddTransient<GenericCachedClassService<MeasurementUnits>>();
            services.AddTransient<GenericCachedClassService<OwnershipForms>>();
            services.AddTransient<GenericCachedClassService<ProductionTypes>>();
            services.AddTransient<GenericCachedClassService<ProductReleasePlans>>();
            services.AddTransient<GenericCachedClassService<Products>>();
            services.AddTransient<GenericCachedClassService<ProductSalesPlans>>();

            services.AddDistributedMemoryCache();
            services.AddMemoryCache();
            services.AddSession();
        }

        private static void TableActivityTypes(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<ActivityTypes> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<ActivityTypes>>();
                var activitys = cachedClassService.GetAll("ActivityTypes20");
                string responseString = "<HTML><TITLE>Таблица вид деятельности</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица вид деятельности</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Имя</TH></TR>" +
                    "</THEAD>";
                foreach (var activity in activitys)
                {
                    responseString += 
                    "<TR>" + 
                        "<TD>" + activity.Id + "</TD>" +
                        "<TD>" + activity.Name + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TableCompanies(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<Companies> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<Companies>>();
                var Companiess = cachedClassService.GetAll("Companies20");
                string responseString = "<HTML><TITLE>Таблица актерские труппы</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица компани</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Имя</TH><TH>Фамилия Имя Отчество</TH><TH>Форма собственности Id</TH><TH>Вид деятельности Id</TH></TR>" +
                    "</THEAD>";
                foreach (var Companies in Companiess)
                {
        responseString +=
                    "<TR>" +
                        "<TD>" + Companies.Id + "</TD>" +
                        "<TD>" + Companies.Name + "</TD>" +
                        "<TD>" + Companies.FIO + "</TD>" +
                        "<TD>" + Companies.OwnershipFormId + "</TD>" +
                        "<TD>" + Companies.ActivityTypeId + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }
        private static void TableMeasurementUnits(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<MeasurementUnits> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<MeasurementUnits>>();
                var MeasurementUnitss = cachedClassService.GetAll("FilmProductions20");
                string responseString = "<HTML><TITLE>Таблица компания-производитель</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица единицы измирения</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Название</TH></TR>" +
                    "</THEAD>";
                foreach (var MeasurementUnits in MeasurementUnitss)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + MeasurementUnits.Id + "</TD>" +
                        "<TD>" + MeasurementUnits.Name + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TableOwnershipForms(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<OwnershipForms> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<OwnershipForms>>();
                var OwnershipFormss = cachedClassService.GetAll("Films20");
                string responseString = "<HTML><TITLE>Таблица фильмы</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица формы собственности</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Название</TH></TR>" +
                    "</THEAD>";
                foreach (var OwnershipForms in OwnershipFormss)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + OwnershipForms.Id + "</TD>" +
                        "<TD>" + OwnershipForms.Name + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TableProductionTypes(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<ProductionTypes> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<ProductionTypes>>();
                var ProductionTypess = cachedClassService.GetAll("ProductionTypes20");
                string responseString = "<HTML><TITLE>Таблица виды продукции </TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица жанры</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>ПродукцияId</TH></TR>" +
                    "</THEAD>";
                foreach (var ProductionTypes in ProductionTypess)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + ProductionTypes.Id + "</TD>" +
                        "<TD>" + ProductionTypes.ProductId + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TableProductReleasePlans(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<ProductReleasePlans> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<ProductReleasePlans>>();
                var ProductReleasePlanss = cachedClassService.GetAll("ProductReleasePlans20");
                string responseString = "<HTML><TITLE>Таблица планы выпуска продукции</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица мероприятия</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Компании Id</TH><TH>Вид продукции</TH><TH>Планируемый объем производства</TH><TH>Фактический объем производства</TH><TH>Квартал</TH><TH>Год</TH></TR>" +
                    "</THEAD>";
                foreach (var ProductReleasePlans in ProductReleasePlanss)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + ProductReleasePlans.Id + "</TD>" +
                        "<TD>" + ProductReleasePlans.CompanyId + "</TD>" +
                        "<TD>" + ProductReleasePlans.ProductionTypeId + "</TD>" +
                        "<TD>" + ProductReleasePlans.PlannedOutputVolume + "</TD>" +
                        "<TD>" + ProductReleasePlans.ActualOutputVolume + "</TD>" +
                        "<TD>" + ProductReleasePlans.QuarterInfo + "</TD>" +
                        "<TD>" + ProductReleasePlans.YearInfo + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TableProducts(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<Products> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<Products>>();
                var Products = cachedClassService.GetAll("Places20");
                string responseString = "<HTML><TITLE>Таблица продукция</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица продукция</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Id Наименование</TH><TH>Характеристика</TH><TH>Единицы измерения</TH><TH>Фото</TH></TR>" +
                    "</THEAD>";
                foreach (var Product in Products)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + Product.Id + "</TD>" +
                        "<TD>" + Product.Name + "</TD>" +
                        "<TD>" + Product.Characteristic + "</TD>" +
                        "<TD>" + Product.MeasurementUnitId + "</TD>" +
                        "<TD>" + Product.Photo + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void TableProductSalesPlans(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                GenericCachedClassService<ProductSalesPlans> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<ProductSalesPlans>>();
                var ProductSalesPlanss = cachedClassService.GetAll("ProductSalesPlans20");
                string responseString = "<HTML><TITLE>Таблица группы сотрудников</TITLE>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY>" +
                "<TABLE BORDER=1 ><CAPTION><B>Таблица планы продаж продукции</B></CAPTION>" +
                    "<THEAD><TR><TH>Id</TH><TH>Компании Id</TH><TH>Виды продукции Id</TH><TH>Планируемый объем производства</TH><TH>Фактический объем производства</TH><TH>Квартал</TH><TH>Год</TH></TR>" +
                    "</THEAD>";
                foreach (var ProductSalesPlans in ProductSalesPlanss)
                {
                    responseString +=
                    "<TR>" +
                        "<TD>" + ProductSalesPlans.Id + "</TD>" +
                        "<TD>" + ProductSalesPlans.CompanyId + "</TD>" +
                        "<TD>" + ProductSalesPlans.ProductionTypeId + "</TD>" +
                        "<TD>" + ProductSalesPlans.PlannedImplementationVolume + "</TD>" +
                        "<TD>" + ProductSalesPlans.ActualImplementationVolume + "</TD>" +
                        "<TD>" + ProductSalesPlans.QuarterInfo + "</TD>" +
                        "<TD>" + ProductSalesPlans.YearInfo + "</TD>" +
                    "</TR>";
                }
                responseString += "</BODY></TABLE></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }
       
        private static void SearchForm1ForGenres(IApplicationBuilder app)
        {
            app.UseSession();
            app.Run(async context =>
            {
                GenericCachedClassService<ActivityTypes> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<ActivityTypes>>();
                var ActivityTypes = cachedClassService.GetAll("ActivityTypes20");
                string keyInputField = "FieldActivityTypes";
                string keySelectField = "SelectActivityTypes";
                string keyRadioFiled = "RadioFiledActivityTypes";

                string responseString = "<HTML><HEAD><TITLE>Форма №1(Сессия)</TITLE></HEAD>" + "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/><BODY>" +
                "<FORM>" +
                "<BR><A href='/'>На главную</A></BR>" +
                "<BR><INPUT type='submit' value='Сохранить'/></BR>" +
                $"<BR><INPUT type='text' name='{keyInputField}' value='{context.Session.GetString(keyInputField)}'/><BR/>" +
                $"<BR><SELECT name='{keySelectField}'><option>default</options></BR>";

                foreach (var ActivityType in ActivityTypes)
                {
                    if ($"{ActivityType.Name}" == context.Session.GetString(keySelectField))
                    {
                        responseString += $"<option selected>{ActivityType.Name}</option>";
                    }
                    else
                    {
                        responseString += $"<option>{ActivityType.Name}</option>";
                    }
                }
                responseString += "</SELECT></BR>";
                responseString += "<BR>";
                foreach (var ActivityType in ActivityTypes)
                {
                    if ($"{ActivityType.Name}" == context.Session.GetString(keyRadioFiled))
                    {
                        responseString += $"<p><INPUT type='radio' checked value='{ActivityType.Name}' name='{keyRadioFiled}'/>{ActivityType.Name}</p>";
                    }
                    else
                    {
                        responseString += $"<p><INPUT type='radio' value='{ActivityType.Name}' name='{keyRadioFiled}'/>{ActivityType.Name}</p>";
                    }
                }
                responseString += "</BR>";
                string ActivityTypesField = context.Request.Query[keyInputField];
                string ActivityTypesSelectedOne = context.Request.Query[keySelectField];
                string ActivityTypesSelected = context.Request.Query[keyRadioFiled];

                if (ActivityTypesField is not null)
                {
                    context.Session.SetString(keyInputField, ActivityTypesField);
                }

                if (ActivityTypesSelectedOne is not null)
                {
                    context.Session.SetString(keySelectField, ActivityTypesSelectedOne);
                }

                if (ActivityTypesSelected is not null)
                {
                    context.Session.SetString(keyRadioFiled, ActivityTypesSelected);
                }
                responseString += "</BODY></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void SearchForm2ForGenres(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                GenericCachedClassService<ActivityTypes> cachedClassService = context.RequestServices.GetRequiredService<GenericCachedClassService<ActivityTypes>>();
                var ActivityTypes = cachedClassService.GetAll("ActivityTypes20");
                string keyInputField = "FieldActivityTypes";
                string keySelectField = "SelectFieldActivityTypes";
                string keyRadioFiled = "RadioFiledActivityTypes";

                string responseString = "<HTML><HEAD><TITLE>Форма №2(Куки)</TITLE></HEAD>" + "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/><BODY>" +
                "<FORM>" +
                "<BR><A href='/'>На главную</A></BR>" +
                "<BR><INPUT type='submit' value='Сохранить'/></BR>" +
                $"<BR><INPUT type='text' name='{keyInputField}' value='{context.Request.Cookies[keyInputField]}'/><BR/>" +
                $"<BR><SELECT name='{keySelectField}'><option>default</options></BR>";

                foreach (var ActivityType in ActivityTypes)
                {
                    if ($"{ActivityType.Name}" == context.Request.Cookies[keySelectField])
                    {
                        responseString += $"<option selected>{ActivityType.Name}</option>";
                    }
                    else
                    {
                        responseString += $"<option>{ActivityType.Name}</option>";
                    }
                }
                responseString += "</SELECT></BR>";
                responseString += "<BR>";
                foreach (var ActivityType in ActivityTypes)
                {
                    if ($"{ActivityType.Name}" == context.Request.Cookies[keyRadioFiled])
                    {
                        responseString += $"<p><INPUT type='radio' checked value='{ActivityType.Name}' name='{keyRadioFiled}'/>{ActivityType.Name}</p>";
                    }
                    else
                    {
                        responseString += $"<p><INPUT type='radio' value='{ActivityType.Name}' name='{keyRadioFiled}'/>{ActivityType.Name}</p>";
                    }
                }
                responseString += "</BR>";
                string ActivityTypesField = context.Request.Query[keyInputField];
                string ActivityTypesSelectedOne = context.Request.Query[keySelectField];
                string ActivityTypesSelected = context.Request.Query[keyRadioFiled];

                if (ActivityTypesField is not null)
                {
                    context.Response.Cookies.Append(keyInputField, ActivityTypesField);
                }

                if (ActivityTypesSelectedOne is not null)
                {
                    context.Response.Cookies.Append(keySelectField, ActivityTypesSelectedOne);
                }

                if (ActivityTypesSelected is not null)
                {
                    context.Response.Cookies.Append(keyRadioFiled, ActivityTypesSelected);
                }
                responseString += "</BODY></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }

        private static void Info(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                string responseString = "<HTML><HEAD><TITLE>Информация</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><h1>Информация:</h1>";
                responseString += "<p> Сервер: " + context.Request.Host + "</p>";
                responseString += "<p> Путь: " + context.Request.PathBase + "</p>";
                responseString += "<p> Протокол: " + context.Request.Protocol + "</p>";
                responseString += "<p> Контекст: " + context.Request.HttpContext + "</p>";
                responseString += "<A href='/'>Главная</A></BODY></HTML>";
                await context.Response.WriteAsync(responseString);
            });
        }
       
    }
}
