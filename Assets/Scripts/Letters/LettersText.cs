using System.Collections.Generic;
using UnityEngine;

public class LettersText : MonoBehaviour
{
    public enum Entry
    {
        NoData,
        L01,
        L02,
        L03,
        L04,
        L05,
        Barn,
        Cook,
        Commander,
        Soldier,
        Worker,
        L06,
        L07,
        L08,
        Final
    }

    public static string Get(Entry entry)
        => text[Locale.language][entry];

    private static readonly Dictionary<Locale.Language, Dictionary<Entry, string>> text
        = new Dictionary<Locale.Language, Dictionary<Entry, string>>()
        {
            [Locale.Language.Ru] = new Dictionary<Entry, string>()
            {
                [Entry.NoData] = "НЕТ ДАННЫХ ВЫСЫЛАЙТЕ ПОДМОГУ",

                [Entry.L01] = "Краем глаза увидел, как одна маленькая девочка приложила руки к стволу дерева, что-то ему шепнула, а дерево выпустило новую веточку. Девочка веточку сорвала и тут же убежала. Деревенские говорят, что это лесные люди." +
                "\n\nХорошие новости, для меня. Академия, конечно, послала меня разведывать лес для заготовок, но я все-таки надеюсь принести пользу и науке",

                [Entry.L02] = "Сегодня меня ждал успех! Заметил в лесу человека. Темная кожа, темные волосы, плетеная одежда, все прямо как в энциклопедии. Попробовал проследить за ним, но быстро обнаружил себя. К счастью, Мар оказался добродушным человеком. Даже говорит на ломаном наречии Наори." +
                "\n\nНапросился к нему в гости. К моему удивлению, он согласился",

                [Entry.L03] = "Спросил Мара про девочку с веточкой. Говорит, это часть их договора с духами леса. Люди Мара заботятся о лесных зверях, а лес помогает людям Мара. " +
                "\n\nМар и меня приютил — позволил жить неподалеку.",

                [Entry.L04] = "Жаль разочаровывать нашего великого лидера, но древесина здесь ни к черту. Деревья тонкие — на строительство не пойдут, да дымят как черт знает что. Индейцы жгут костры круглосуточно — часть их ритуалов. Дым, что странно, глаза не режет." +
                "\n\nЗато кафедра антропологии будет рада моему докладу.",

                [Entry.L05] = "Кафедра антропологии действительно была рада моему отчету. Видимо, я наткнулся на что-то крупное. Высылают  экспедицию с поваром, работниками и солдатами. " +
                "\n\nЭто только помешает мне. Культуру исследовать нужно изнутри, а не из-за частокола.",

                [Entry.Barn] = "Тащить древесину в лес, это же надо такое придумать? Хотя, тут и правда с этим не густо. На частокол еще может и есть что нарубить, а домишки то строить уже не из чего. " +
                "\n\nДа и работничков, по хорошему, надо было с собой вести, а не из местных селян. Построили все наспех, даже землю не разровняли. А у меня теперь свитки скатываются.",

                [Entry.Cook] = "Пойди пойми этих деревенских. Куру им солдатскую даю, а они не хотят — кривятся. Сидят по углам своим да репу жрут, как звери, чесслово.",

                [Entry.Commander] = "Арно отказывается жить в лагере и отчитываться передо мной (цитата). Первое я понимаю — казарменная жизнь не для академиков. Второе же приносит мне неудобства — без его знаний мы будто ходим в тумане. Точнее, в дыму." +
                "\n\nЖаль солдат. Академия хочет, чтобы мы обуздали дикую магию. Как у нас это получится, если единственный эксперт поблизости не хочет сотрудничать? Но командование не терпит промедления — придется рисковать парнями.",

                [Entry.Soldier] = "Я сперва был рад что нас сюда направили. Скачи себе вокруг костра весь день, спи, да жри, прямо сказка а не служба. А теперь страшно. " +
                "\n\nМихай вот из лагеря пропал, пока мы костер жгли. В лесу его потом нашли, да только никто и не понял, как он там оказался. Михай молчит, да вперед смотрит. Надо бы Милке его письмецо черкануть. Кажись, светит им теперь пенсия пожизненная. Вот она рада будет.",

                [Entry.Worker] = "Не понимаю я эту солдатню. Вроде взрослые все, а вокруг костра хороводы водят, словно праздник какой. Да только не бывает таких долгих праздников." +
                "\n\nХорошо хоть меня отпущают. Довезу этого их раненого до крепости, да можно и домой.",

                [Entry.L06] = "Эти рубаки лес валят, словно то — враг государев. Почто им та древесина? Солдатам хорошо, весело, а лес вокруг них худеет, отступает с каждым днем. Командир хоть читать обучен, да все туда же — верности приказам больше чем ума." +
                "\n\nКаменное лицо Мара не выражает и тени беспокойства. Мне стыдно перед ним. Мар успокаивает меня. Говорит, лес со всем справятся. ",

                [Entry.L07] = "После распросов командира запросил из академии все книги что есть по религиям кочевых и лесных народов. Пришла только одна, да с запиской — «по старой дружбе». Почерк незнаком. Нашел упоминания о похожих обрядах. Те индейцы «ходили сквозь туман». Мол, каждый костер был для них входом, а дым — выходом. Сперва это показалось мне чушью, детской сказкой. А что если правда? Мар не ответил.",

                [Entry.L08] = "Люди Мара уходят. В тумане растворяются целые семьи, одна за другой. Кажется, они уходят навсегда. Вчера я видел, как деревья усыхают от их прикосновений — неприятное зрелище. Мне тяжело не винить себя за это.",

                [Entry.Final] = "Не знаю, насколько успешны эксперименты, но туман вокруг лагеря не рассеивается несколько дней, только стелется все дальше и дальше. Боюсь, пути назад для меня уже нет."
            },

            [Locale.Language.En] = new Dictionary<Entry, string>()
            {
                [Entry.NoData] = "NO DATA SEND HELP",

                [Entry.L01] = "Out of the corner of my eye I saw how one little girl put her hands to the trunk of a tree, whispered something to it, and the tree released a new twig.The girl picked a twig and ran away right away.The villagers say that girl was on of the forest people." +
                "\n\nThat is good news for me. Academy, of course, sent me to scout the forest for harvesting, but I still hope to benefit science as well",

                [Entry.L02] = "Today, success was waiting for me! I noticed a man in the forest. Dark skin, dark hair, woven clothes, everything was just like in an encyclopedia. I tried to follow him, but was detected quickly. Fortunately, Mar turned out to be a good-natured person. He even speaks in broken Naori . " + 
                "\n\nAsked to visit him. To my surprise, he agreed",

                [Entry.L03] = "Asked Mar about a girl with a twig. He says this is part of their contract with the spirits of the forest. The people of Mar care for the forest animals, and the forest helps the people of Mar. " + 
                "\n\nMar also cared for me — allowed me to live nearby.",

                [Entry.L04] = "It is a pity to disappoint our great leader, but the local wood is not good at all. The trees are thin - they won’t go for construction, but smoke like hell knows what. Indians burn bonfires around the clock - part of their rituals. Smoke, which is strange, does not hurt the eyes. " + 
                "\n\nThe Department of Anthropology will be delighted with my report, I hope.",

                [Entry.L05] = "The Department of Anthropology was really pleased with my report. Apparently, I came across something big. They send an expedition with a cook, workers and soldiers. " + 
                "\n\nThis will only hinder me. I need to explore the culture from the inside, rather than hiding behind the stockade.",

                [Entry.Barn] = "To drag wood into the forest, isn't that stupid? Although, there really isn’t much of good wood here. There might be something to chop into the picket fence, but there’s nothing to build houses from." +  
                "\n\nYes, and the workers, why didn't we bring our own guys? Those peasants built everything hastily, even the land was not leveled. And now my scrolls are rolling down.",

                [Entry.Cook] = "I don't understand those country boys. I give the soldier’s chicken to them, but they don’t want to eat it. They sit in their corners and eat turnips, like some animals.",

                [Entry.Commander] = "Arno refuses to live in the camp and report to me (quote). The first I understand - barracks life is not for academics. The second brings me inconvenience - without his knowledge we seem to walk in a fog. More precisely, in the smoke." + 
                "\n\nI pity the soldiers. The Academy wants us to curb wild magic. How can we do this if the only expert nearby doesn’t want to cooperate? But the command cannot tolerate delay - we have to risk guys lives.",

                [Entry.Soldier] = "At first I was glad that we were sent here. Jump around the campfire all day, sleep, eat, just a fairy tale and not a service. And now it's scary. " + 
                "\n\nMikhai disappeared from the camp while we were burning a bonfire. Then we found him in forest, but no one understood how he got there. Mihai is silent, doesn't react at us at all. I should scribbled a letter to his Milka. It seems they are now on a lifelong pension, so she will be glad.",

                [Entry.Worker] = "I do not understand this soldiery. Dance around the fire like it's some festival. But there are no such long festivals. " + 
                "\n\nGood though they let me go. I’ll bring this one sick man to the fortress, and can go home.",

                [Entry.L06] = "These fighters cut down the forest, as if it were an enemy of the sovereign. Why do they want that wood? The soldiers are having fun, and the forest around them is losing weight, retreating every day. The commander, at least trained to read, but still one of them - more loyalty to orders then common sense" + 
                "\n\nMar’s stony face doesn’t even express a shadow of concern. I am ashamed. Mar reassures me. He says the forest can handle everything.",

                [Entry.L07] = "After questioning of the commander I have requested all the books that are on the religions of nomadic and forest peoples from the academy. Only one came, but with a note - «according to the old friendship.» Handwriting unfamiliar. " +
                "\n\n Found references to similar rites. Those Indians «walked through the fog.» Say, every fire was an entrance for them, and smoke was an exit. At first it seemed to me nonsense, a children's fairy tale. What if it is the truth? Mar did not answer.",

                [Entry.L08] = "The people of Mar are leaving. Entire families dissolve in the fog, one after another. They seem to be gone forever. Yesterday, I saw trees dry out from their touch - an unpleasant sight. It's hard for me not to blame myself.",

                [Entry.Final] = "I don’t know how successful the experiments are, but the fog around the camp does not dissipate for several days, it just spreads farther and farther. I'm afraid there is no turning back for me."
            }
        };
}
