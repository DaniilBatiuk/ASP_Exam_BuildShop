using ASP_Meeting_18.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Drawing2D;
using Topshelf.Runtime;

namespace ASP_Meeting_18.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider,
            IWebHostEnvironment hostEnvironment)
        {
            DbContextOptions<ShopDbContext> options = serviceProvider
                .GetRequiredService<DbContextOptions<ShopDbContext>>();
            using (ShopDbContext context = new ShopDbContext(options))
            {
                context.Database.EnsureCreated();
                if (context.Categories.Any())
                    return;
                Category[] categories =
                {
                    new Category {Title = "Газобетон, Кирпич, Шлакоблок" },
                    new Category {Title = "Газобетон", ParentCategoryId = 1 },
                    new Category {Title = "Кирпич", ParentCategoryId = 1 },
                    new Category {Title = "Шлакоблок", ParentCategoryId = 1 },
                    new Category { Title = "Багеты, Плинтуса, Пластик" },
                    new Category { Title = "Багеты", ParentCategoryId = 5 },
                    new Category { Title = "Плинтуса", ParentCategoryId = 5 },
                    new Category { Title = "Пластик", ParentCategoryId = 5 },
                    new Category { Title = "Гипсокартон и комплектующие" },
                    new Category { Title = "Гипсокартон", ParentCategoryId = 9 },
                    new Category { Title = "Подвесы", ParentCategoryId = 9 }
                };
                context.Categories.AddRange(categories); 
                Product[] products =
                {
                    new Product{
                        Title = "Багет Vidella O - 45 2 м",
                        Description = "Для создания идеального декора, прекрасного интерьера и комфортной атмосферы хорошо подходят багеты Vidella . " +
                        "Данный потолочный плинтус обладает приятным внешним видом и хорошим качеством. Несмотря на доступную стоимость изделия, оно отличается долговечностью службы и устойчивостью к " +
                        "механическим повреждениям.",
                        Price = 15.00,
                        Count=100,
                        CategoryId=6,
                    },
                    new Product{
                        Title = "Багет Vidella С - 20 2 м",
                        Description = "Для создания идеального декора, прекрасного интерьера и комфортной атмосферы хорошо подходят багеты Vidella . Данный потолочный плинтус обладает " +
                        "приятным внешним видом и хорошим качеством. Несмотря на доступную стоимость изделия, оно отличается долговечностью службы и устойчивостью к механическим повреждениям.",
                        Price = 11.00,
                        Count=100,
                        CategoryId=6,
                    },
                    new Product{
                        Title = "Багет Vidella С - 30 2 м",
                        Description = "Для создания идеального декора, прекрасного интерьера и комфортной атмосферы хорошо подходят багеты Vidella . Данный потолочный плинтус обладает " +
                        "приятным внешним видом и хорошим качеством. Несмотря на доступную стоимость изделия, оно отличается долговечностью службы и устойчивостью к механическим повреждениям.",
                        Price = 19.00,
                        Count=100,
                        CategoryId=6,
                    },
                    new Product{
                        Title = "Плинтус LinePlast L007 Ясень Шимо Светлый 58мм * 2,5м",
                        Description = "В процессе завершения интерьера очень важно позаботиться об установке качественного плинтуса. Так, напольный плинтус LinePlast является прекрасным " +
                        "решением для создания комфортной атмосферы в помещении. Без него очень трудно представить завершение ремонта. Благоустройство квартиры или дома при помощи этого " +
                        "изделия - незаменимое решение не только современных дизайнеров, но и обычных строителей. Стоимость плинтуса у нас на порядок ниже, чем у сайтов конкурентов.",
                        Price = 47.00,
                        Count=100,
                        CategoryId=7,
                    },
                    new Product{
                        Title = "Плинтус LinePlast L068 Фраке 58мм * 2,5м",
                        Description = "В процессе завершения интерьера очень важно позаботиться об установке качественного плинтуса. Так, напольный плинтус LinePlast является прекрасным " +
                        "решением для создания комфортной атмосферы в помещении. Без него очень трудно представить завершение ремонта. Благоустройство квартиры или дома при помощи этого " +
                        "изделия - незаменимое решение не только современных дизайнеров, но и обычных строителей. Стоимость плинтуса у нас на порядок ниже, чем у сайтов конкурентов.",
                        Price = 47.00,
                        Count=100,
                        CategoryId=7,
                    },
                    new Product{
                        Title = "Пластик RICO 100 мм белый Вагонка полоса 6 м",
                        Description = "Пластиковые декоративные стеновые панели с плёнкой ПВХ становятся всё более популярными. Плёнки ПВХ изготавливаются различных расцветок с имитацией " +
                        "благородных пород древесины, камня, мрамора и тд. С помощью рисунков на ПВХ плёнке можно реализовать любую дизайнерскую идею.",
                        Price = 140.00,
                        Count=100,
                        CategoryId=8,
                    },
                    new Product{
                        Title = "Пластик белый лак, ширина 250 мм, длина 6 м, толщина 7 мм",
                        Description = "Пластиковые декоративные стеновые панели с плёнкой ПВХ становятся всё более популярными. Плёнки ПВХ изготавливаются различных расцветок с имитацией " +
                        "благородных пород древесины, камня, мрамора и тд. С помощью рисунков на ПВХ плёнке можно реализовать любую дизайнерскую идею.",
                        Price = 415.00,
                        Count=100,
                        CategoryId=8,
                    },
                    new Product{
                        Title = "Гипсокартон Knauf Потолочный 9.5*1200*2500 мм",
                        Description = "Гипсокартон ценится среди домашних мастеров и профессиональных строителей за высокие технические характеристики. Панели, производимые украинской " +
                        "компанией Knauf, имеют параметры 2500х1200х9,5 миллиметров, а также площадь в 3 квадратных метра. Благодаря таким габаритам процесс монтажа проходит легко и быстро." +
                        " Кроме того, изделия выдерживают колоссальные нагрузки, спокойно контактируют с внешними факторами.",
                        Price = 369.00,
                        Count=100,
                        CategoryId=10,
                    },
                    new Product{
                        Title = "Гипсокартон Knauf стеновой 12.5*1200*3000 мм",
                        Description = "Гипсокартон крупного производителя Украины Knauf является уникальным отделочным материалом. Расходник размерами 3000х1200х12,5 миллиметров, " +
                        "площадью в 3,6 квадратных метра и весом 29 килограммов отлично взаимодействует с другими строительными материалами. На него можно класть краску, шпаклевку " +
                        "или клей для проведения дальнейших отделочных мероприятий.",
                        Price = 453.00,
                        Count=100,
                        CategoryId=10,
                    },
                    new Product{
                        Title = "Подвес Стержень Петля (250, 500, 1000 мм)",
                        Description = "Конструкция изготовлена из высококачественной стали, покрытой оцинковкой, поэтому хорошо переносит специфические условия. " +
                        "Способна контактировать с влагой на протяжении длительного времени. Не восприимчива к химической среде, что позволяет изделию держаться десятилетиями." +
                        " Подвес «стержень-петля» с длиной 125, 250, 500 и 1000 миллиметров предназначен для сооружения каркасов для гипсокартонных плит. ",
                        Price = 2.00,
                        Count=100,
                        CategoryId=11,
                    },
                    new Product{
                        Title = "Подвес Стержень Крюк (125, 250 мм)",
                        Description = "Конструкция изготовлена из высококачественной стали, покрытой оцинковкой, поэтому хорошо переносит специфические условия. " +
                        "Способна контактировать с влагой на протяжении длительного времени. Не восприимчива к химической среде, что позволяет изделию держаться десятилетиями." +
                        " Подвес «стержень-петля» с длиной 125, 250, 500 и 1000 миллиметров предназначен для сооружения каркасов для гипсокартонных плит. ",
                        Price = 2.00,
                        Count=100,
                        CategoryId=11,
                    },
                    new Product{
                        Title = "Газобетон UDK 100*200*600 мм",
                        Description = "Газобетон пользуется большой популярностью благодаря своим высоким техническим характеристикам. Нужно отметить, что прочность, морозостойкость " +
                        "класса F100 и долговечность были достигнуты за счет уникальной формулы приготовления, а точнее – пор внутри.",
                        Price = 45.60,
                        Count=100,
                        CategoryId=2,
                    },
                    new Product{
                        Title = "Кирпич белый силикатный одинарный Харьков",
                        Description = "Силикатный кирпич белого цвета отличается хорошими техническими свойствами. Изделие размерами 119х65х248 миллиметров обладает повышенной морозостойкостью," +
                        " прочностью, долговечностью, устойчивостью к окружающей среде. Также стоит отметить, что стройматериал оказывает бактерицидное воздействие за счет добавленной в процессе" +
                        " изготовления извести.",
                        Price = 10.50,
                        Count=100,
                        CategoryId=3,
                    },
                    new Product{
                        Title = "Кирпич красный полнотелый М-100 Малая Токмачка",
                        Description = "Полнотелый красный кирпич М100 размерами 120х70х250 миллиметров и весом 3,2 килограмма имеет высокие технические характеристики. Постройки получаются " +
                        "прочными и долговечными. Стены выдерживают морозы и таяние льда, открытый огонь, не пропускают шумы, хорошо переносят воздействие внешних факторов. ",
                        Price = 14.00,
                        Count=100,
                        CategoryId=3,
                    },
                    new Product{
                        Title = "Шлакоблок с квадратным отверстием 200*200*400 мм",
                        Description = "Стеновой шлакоблок с квадратными отверстиями и весом в 21,5 килограмм применяется в домашнем, коммерческом и промышленном строительстве. " +
                        "За счет технических характеристик стройматериала (шумоизоляция, прочность, морозостойкость, устойчивость к внешним факторам, долговечность) конструкция простоит много лет. ",
                        Price = 35.00,
                        Count=100,
                        CategoryId=4,
                    },
                };
                products[0].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\5906735639612_full.jpg") });
                products[1].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\2.jpg") });
                products[2].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\3.jpg") });
                products[3].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\4.png") });
                products[4].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\5.png") });
                products[5].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\6.png") });
                products[6].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\7.jpg") });
                products[7].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\12.jpg") });
                products[8].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\13.jpg") });
                products[9].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\14.jpg") });
                products[10].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\15.jpg") });
                products[11].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\11.jpg") });
                products[12].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\9.jpg") });
                products[13].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\10.jpg") });
                products[14].Images!.Add(new Photo { Image = File.ReadAllBytes($"{hostEnvironment.WebRootPath}\\images\\8.jpg") });
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
