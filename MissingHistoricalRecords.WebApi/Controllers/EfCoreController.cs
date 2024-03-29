﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MissingHistoricalRecords.WebApi.Models;
using MissingHistoricalRecords.WebApi.Repository;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MissingHistoricalRecords.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EfCoreController : ControllerBase
    {
        private readonly EfCoreRepository _efCore;
        public EfCoreController(EfCoreRepository efCore)
        {
            _efCore = efCore;
        }
        [HttpGet("books")]
        public ActionResult<IEnumerable<BookModel>> GetBooks()
        {
            var books = _efCore.GetBooks();
            return Ok(books);
        }
        [HttpPost("books")]
        public ActionResult<IEnumerable<BookModel>> CrateBook(BookModel createModel)
        {
            var result = _efCore.CreateBook(createModel);
            var msg = result > 0 ? "Update success" : "Update fail";
            return Ok(msg);
        }
        [HttpPost("books/seed")]
        public ActionResult<IEnumerable<BookModel>> CrateBook()
        {
            var json = @"[
  {
    ""BookId"": 1,
    ""BookTitle"": ""အင်းဝ ၊ ဗားကရာနှင့် မယ်နုအုတ်ကျောင်း"",
    ""BookAuthor"": ""သော်တာမင်း"",
    ""BookCover"": ""book-cover/1.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""မြန်မာ့သမိုင်းတွင် ခရစ်နှစ် ၁၃၆၄ ခုနှစ်မှ ၁၇၅၁ ခုနှစ်အထိ နှစ် ၄ဝဝ နီးပါး သက်တမ်းရှည်ကြာခဲ့ပြီး မင်းနေပြည်တော် လေးကြိမ်ဖြစ်ခဲ့သည့် အင်းဝ သည် ပြည်တွင်းပြည်ပ၌ အထူးထင်ရှားသော သမိုင်းဝင်မြို့ဟောင်းတစ်ခုဖြစ်သည်။ အနောက် နိုင်ငံသားများက ပထမမြန်မာနိုင်ငံတော်(ပုဂံ ခေတ်)ကို Kingdom of Pagan ဟုလည်း ကောင်း၊ ဒုတိယမြန်မာနိုင်ငံတော်(တောင်ငူ ခေတ်)ကို Kingdom of Pegu ဟု လည်း ကောင်း၊ တတိယမြန်မာနိုင်ငံတော်(ကုန်းဘောင် ခေတ်)ကို Kingdom of Ava, Court of Ava ဟု လည်းကောင်း ရေးသားလေ့ရှိခဲ့ပြီး အင်းဝဖြစ်စေ၊ အမရပူရဖြစ်စေ နေပြည်တော်ကို အင်းဝ(Ava)ဟုသာ မှတ်တမ်းပြုခဲ့ကြသည်။""
  },
  {
    ""BookId"": 2,
    ""BookTitle"": ""ရန်ကင်းတောင်(သို့)ငါးရံမင်းတောင်"",
    ""BookAuthor"": ""သော်တာမင်း"",
    ""BookCover"": ""book-cover/2.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""ပုဂံမင်းနေပြည်တော်တွင် သက္ကရာဇ် ၄၅၄ ခုနှစ်မှ ၅၂၉ ခုနှစ်ထိ၊ ၇၅ နှစ်တိုင်တိုင် အလောင်းစည်သူမင်းဘုရင်အဖြစ်အုပ်ချု ုပ်မင်းလုပ်ခဲ့သည်။အလောင်းစည်သူမင်းနှင့်ရတနာပုံမိဖုရားတို့၏ ""
  },
  {
    ""BookId"": 3,
    ""BookTitle"": ""မင်းကွန်းပုထိုးတော်ကြီး"",
    ""BookAuthor"": ""သော်တာမင်း"",
    ""BookCover"": ""book-cover/3.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""တည်နေရာ စစ်ကိုင်းတိုင်း၊ မင်းကွန်းမြို့\n\n    ၁၇၉၀ ပြည့်က သူ့နန်းဌါနသို့ အလည်ရောက်လာ စိနတိုင်း ဘုရင် ကိုယ်စားလှယ်တော် တမန်အဖွဲ့က ပဏ္ဏပါးသည့် ဗုဒ္ဓစွယ်တော် တဆူ ဌာပနာတော် မူရန် ကမ္ဘာ့အကြီးဆုံး ပုထိုးတစ်ဆူ တည်တော်မူရန် ဘိုးတော်ဘုရား ရည်သန်တော် မူခဲ့သည်။""
  },
  {
    ""BookId"": 4,
    ""BookTitle"": ""ကုသိုလ်တော်ဘုရား"",
    ""BookAuthor"": ""သော်တာမင်း"",
    ""BookCover"": ""book-cover/4.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""ကျောက်ထက်အက္ခရာတင်ပုံ…\n    …မင်းတုန်းမင်းကြီးက ဘုရားနှုတ်ကပါဌ်တော်တွေကို ကမ္ဘာတည် တာနဲ့အမျှ မပျောက်မပျက် တည်ရှိနေအောင် သာသနာပတဲ့အချိန်အထိ တည်စေဖို့ ကျောက်ပေါ်မှာ ရေးထိုးထားခဲ့ချင်တာဆိုတော့""
  },
  {
    ""BookId"": 5,
    ""BookTitle"": ""ကောင်းမှုတော်ဘုရားအကြောင်း သိကောင်းစရာ"",
    ""BookAuthor"": ""သော်တာမင်း"",
    ""BookCover"": ""book-cover/5.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""ကောင်းမှုတော် ဘုရားသည် စစ်ကိုင်း၏ အကျော်ကြားဆုံး အနက်တဆူဖြစ်၍ စစ်ကိုင်းမြို့ မြောက်ဘက် (၆) မိုင်ခန့်အကွာ၌ တည်ရှိပြီး ဘွဲ့အမည်မှာ ရာဇမဏိစူဠာ ဖြစ်သည်။ မြန်မာသက္ကရာဇ် (၉၉၈) ခုနှစ် ၊ ခရစ်နှစ် (၁၆၃၆) ၊ တော်သလင်းလဆန်း (၁၀) ရက် ၊ တနင်္လာနေ့တွင် သတိုးဓမ္မရာဇာ ဘွဲ့ခံ သာလွန်မင်းတရားကြီး တည်ထားကိုးကွယ်ခဲ့သည်။""
  },
  {
    ""BookId"": 6,
    ""BookTitle"": ""စူဠာမဏိ (သို့မဟုတ်) ဘုရင်နှင့်ပြည်သူ ကြည်ဖြူသည့်ဘုရား"",
    ""BookAuthor"": ""သော်တာမင်း"",
    ""BookCover"": ""book-cover/6.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""နရပတိစည်သူမင်းကြီးသည် စူဠာမဏိစေတီကို ခရစ်နှစ် ၁၁၈၃-ခုနှစ်တွင်တည်ခဲ့လေသည်။ ထို့ကြောင့် ဘုရားသက်တမ်းသည် ယခုဆိုလျှင် ၈၃၅-နှစ် ရှိသွားပြီ ဖြစ်သည်။ ထေရ်ကြီးဝါကြီး စေတီကြီး တစ်ဆူ ဖြစ်သောကြောင့် သမိုင်းတန်ဖိုး ဖြတ်လို့ မရပါ။""
  },
  {
    ""BookId"": 7,
    ""BookTitle"": ""ဓမ္မရံကြီးဘုရား သမိုင်း"",
    ""BookAuthor"": ""သော်တာမင်း"",
    ""BookCover"": ""book-cover/7.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""ထီးနန်းရဖို့ ခမည်းတော်နဲ့နောင်တော်ကိုသတ်\n    အရက်စက်ဆုံး ဘုရင်ရယ်လို့ သမိုင်းတွင်တဲ့\n    နရသူ နဲ့ နရသူ၏ကောင်းမှုတော် ဓမ္မရံကြီးဘုရား\n    ""
  },
  {
    ""BookId"": 8,
    ""BookTitle"": ""ပုဂံ ရွှေစည်းခုံစေတီတော် ဘုရားသမိုင်း"",
    ""BookAuthor"": ""သော်တာမင်း"",
    ""BookCover"": ""book-cover/8.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""ပုဂံပြည့်ရှင် အနော်ရထာမင်းလက်ထက်တွင် သီဟိုဠ်ကျွန်း၏ဘုရင် ပထမဝိဇယဗာဟု မင်းကြီး၏ အကူအညီတောင်းခြင်းကြောင့် ရဟန်းသံဃာတော်များ၊ ပိဋကတ်သုံးပုံ ကျမ်းစာများနှင့် ဆင်ဖြူတော်တစ်စီးတို့ကို အကူအညီပေးခဲ့သည်။""
  },
  {
    ""BookId"": 9,
    ""BookTitle"": ""၃၇ မင်းနတ်သမိုင်း"",
    ""BookAuthor"": ""တင်နိုင်တိုး"",
    ""BookCover"": ""book-cover/9.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""သုံးပန်လှနတ်အကြောင်း""
  },
  {
    ""BookId"": 10,
    ""BookTitle"": ""တန့်ကြည့်တောင်ဘုရား"",
    ""BookAuthor"": ""သော်တာမင်း"",
    ""BookCover"": ""book-cover/10.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""လောကနန္ဒာ ဘုရားကုန်းပေါ်က မျှော်လိုက် ပါလျှင် သာယာ လှပသော\n    ဧရာဝတီမြစ် တစ်ဘက်ကမ်းမှ တန့်ကြည့်တောင်တန်းကြီးကို လည်း\n    ကောင်း""
  },
  {
    ""BookId"": 11,
    ""BookTitle"": ""ထီးလိုမင်းလိုဘုရား"",
    ""BookAuthor"": ""သော်တာမင်း"",
    ""BookCover"": ""book-cover/11.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""ထီးလိုမင်းလိုဘုရားသည် မြန်မာနိုင်ငံ၊ မန္တလေးတိုင်းဒေသကြီး၊ ပုဂံရှိ ဂူဘုရားတစ်ဆူ ဖြစ်ပြီး ပုဂံ-ညောင်ဦးကားလမ်းဘေးတွင် တည်ရှိလေသည်။ ထီးလိုမင်းလိုဘုရားသည် ကွမ်းတောင်ပေါက်လိုဏ် သုံးဆင့်ဂူဘုရား ဖြစ်ပြီး (၁၈)မတ်(၁၂၀၈) တွင် ဇေယျသိင်္ခ နားတောင်းများမင်း (ထီးလိုမင်းလို) အေဒီ (၁၂၁၁-၁၂၃၁) တည်ခဲ့သည်။""
  },
  {
    ""BookId"": 12,
    ""BookTitle"": ""ကန်တော့ပလ္လင် ဘုရားသမိုင်း"",
    ""BookAuthor"": ""သော်တာမင်း"",
    ""BookCover"": ""book-cover/12.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""သက္ကရာဇ် ၅၆၅ ခုနှစ် ကန်တော့ပလ္လင် စေတီတော်ကို နရပတိစည်သူမင်းကြီး တည်ထား ကိုးကွယ်တော်မူ၏။ ""
  },
  {
    ""BookId"": 13,
    ""BookTitle"": ""နီကိုလာ တက်စလာ(၁၈၅၆-၁၉၄၃)"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/13.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 14,
    ""BookTitle"": ""ရိုက်ညီနောင် – အိုဗာ (၁၈၇၁-၁၉၄၂)၊ ဝီဗာ (၁၈၆၇-၁၉၁၂)"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/14.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 15,
    ""BookTitle"": ""ဘင်ဂျမင် ဖရန်ကလင်း (၁၇၀၅-၁၇၉၀)"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/15.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 16,
    ""BookTitle"": ""အာခီမီးဒီးစ် (၂၈၇-၂၁၂ဘီစီ)"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/16.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 17,
    ""BookTitle"": ""လီယိုနာဒို ဒါဗင်ချီ (၁၄၅၂-၁၅၁၉)"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/17.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 18,
    ""BookTitle"": ""ဂဂျိုဟန်နက်စ် ဂူတင်ဘတ် (၁၃၉၂-၁၄၆၈)"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/18.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 19,
    ""BookTitle"": ""ဂျိမ်းစ်ဝပ် (၁၇၃၆-၁၈၁၉)"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/19.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""ဗြိတိန် အင်ဂျင်နီယာ""
  },
  {
    ""BookId"": 20,
    ""BookTitle"": ""ဂယ်လီလီယို ဂယ်လီလဲအီ (၁၅၆၄-၁၆၄၂)"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/20.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 21,
    ""BookTitle"": ""တင်မ် ဘာနာလီ (၁၉၅၅-ယနေ့အထိ)"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/21.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 22,
    ""BookTitle"": ""သောမတ် အယ်လ်ဗာ အက်ဒီဆင် (၁၈၄၇-၁၉၃၁)"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/22.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 23,
    ""BookTitle"": ""ဗိဿနိုးဘုရင်မပန်ထွာနှင့် သူ၏သစ္စာတော်ခံများ"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/23.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 24,
    ""BookTitle"": ""၃၇ မင်းနတ်သမိုင်း"",
    ""BookAuthor"": ""တင်နိုင်တိုး"",
    ""BookCover"": ""book-cover/24.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""သိကြားမင်း အကြောင်း""
  },
  {
    ""BookId"": 25,
    ""BookTitle"": ""ပုဂံနန်းဆက်ပျက်သုန်းခြင်း"",
    ""BookAuthor"": ""ပြည့်စုံ(တောင်ငူ)"",
    ""BookCover"": ""book-cover/25.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 26,
    ""BookTitle"": ""၃၇ မင်းနတ်သမိုင်း"",
    ""BookAuthor"": ""တင်နိုင်တိုး"",
    ""BookCover"": ""book-cover/26.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""မဟာဂီရိ နတ်အကြောင်း""
  },
  {
    ""BookId"": 27,
    ""BookTitle"": ""ကုန်းဘောင်မင်းဆက်၏ ပထမဆုံးမိဖုရားခေါင် စန္ဒာဒေဝီ"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/27.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 28,
    ""BookTitle"": ""၃၇ မင်းနတ်သမိုင်း"",
    ""BookAuthor"": ""တင်နိုင်တိုး"",
    ""BookCover"": ""book-cover/28.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""နှမတော် နတ်အကြောင်း""
  },
  {
    ""BookId"": 29,
    ""BookTitle"": ""ဒုတိယမြန်မာနိုင်ငံတော်ကို ဦးဆုံးတည်ထောင်ခဲ့သူ တပင်ရွှေထီး"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/29.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""တပင်ရွှေထီး""
  },
  {
    ""BookId"": 30,
    ""BookTitle"": ""ဗိဿနိုးဘုရင်မပန်ထွာနှင့် သူ၏သစ္စာတော်ခံများ"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/30.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 31,
    ""BookTitle"": ""မြန်မာ့ လှည်းယဉ်"",
    ""BookAuthor"": ""ဆရာရန်နောင်စိုး"",
    ""BookCover"": ""book-cover/31.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 32,
    ""BookTitle"": ""ထီးနန်းမရသော ကုန်းဘောင်အိမ်ရှေ့မင်း (၃)ပါး"",
    ""BookAuthor"": ""တင်နိုင်တိုး"",
    ""BookCover"": ""book-cover/32.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 33,
    ""BookTitle"": ""၃၇ မင်းနတ်သမိုင်း"",
    ""BookAuthor"": ""တင်နိုင်တိုး"",
    ""BookCover"": ""book-cover/33.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""သုံးပန်လှ နတ်အကြောင်း""
  },
  {
    ""BookId"": 34,
    ""BookTitle"": ""စကြာဝတေးမင်း"",
    ""BookAuthor"": ""ပြည့်စုံ(တောင်ငူ)"",
    ""BookCover"": ""book-cover/34.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 35,
    ""BookTitle"": ""နတ်သျှင်နောင် နှင့် ရာဇဓာတုကလျာ"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/35.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 36,
    ""BookTitle"": ""သီပေါမင်း"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/36.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 37,
    ""BookTitle"": ""အလောင်းစည်သူမင်းတရား"",
    ""BookAuthor"": """",
    ""BookCover"": ""book-cover/37.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": """"
  },
  {
    ""BookId"": 38,
    ""BookTitle"": ""၃၇ မင်းနတ်သမိုင်း"",
    ""BookAuthor"": ""တင်နိုင်တိုး"",
    ""BookCover"": ""book-cover/38.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""မိနှဲမိ နတ်အကြောင်း""
  },
  {
    ""BookId"": 39,
    ""BookTitle"": ""၃၇ မင်းနတ်သမိုင်း"",
    ""BookAuthor"": ""တင်နိုင်တိုး"",
    ""BookCover"": ""book-cover/39.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""ရွှေနပေ နတ်အကြောင်း""
  },
  {
    ""BookId"": 40,
    ""BookTitle"": ""၃၇ မင်းနတ်သမိုင်း"",
    ""BookAuthor"": ""တင်နိုင်တိုး"",
    ""BookCover"": ""book-cover/40.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""တောင်မင်းကြီး ရှင်ညို နတ်အကြောင်း""
  },
  {
    ""BookId"": 41,
    ""BookTitle"": ""၃၇ မင်းနတ်သမိုင်း"",
    ""BookAuthor"": ""တင်နိုင်တိုး "",
    ""BookCover"": ""book-cover/41.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""မြောက်မင်း ရှင်ဖြူ နတ်အကြောင်း""
  },
  {
    ""BookId"": 42,
    ""BookTitle"": ""၃၇ မင်းနတ်သမိုင်း"",
    ""BookAuthor"": ""တင်နိုင်တိုး"",
    ""BookCover"": ""book-cover/42.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""ထီးဖြူဆောင်း နတ်အကြောင်း""
  },
  {
    ""BookId"": 43,
    ""BookTitle"": ""၃၇ မင်းနတ်သမိုင်း"",
    ""BookAuthor"": ""တင်နိုင်တိုး"",
    ""BookCover"": ""book-cover/43.jpg"",
    ""BookCategory"": ""History"",
    ""BookDescription"": ""ထီးဖြူဆောင်းမယ်တော် နတ်အကြောင်း""
  }
]";
            var count = 0;
            var books = JsonConvert.DeserializeObject<List<BookModel>>(json);
            foreach (var book in books)
            {
                count += _efCore.CreateBook(book);

            }
            return Ok(count);
        }
        [HttpGet("books/{id}")]
        public ActionResult<BookModel> GetBook(int id)
        {
            var book = _efCore.GetBook(id);
            if (book is null)
            {
                return NotFound("No record found.");
            }
            return Ok(book);
        }
        [HttpPut("books/{id}")]
        public IActionResult UpdateBook(int id, BookModel editModel)
        {
            var book = _efCore.GetBook(id);
            if (book is null)
            {
                return NotFound("No record found.");
            }
            var result = _efCore.UpdateBook(id, editModel);
            var msg = result > 0 ? "Update success" : "Update fail";
            return Ok(msg);
        }
        [HttpDelete("books/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _efCore.GetBook(id);
            if (book is null)
            {
                return NotFound("No record found");
            }
            var result = _efCore.DeleteBook(book);
            var msg = result > 0 ? "Delete success" : "Delete fail";
            return Ok(msg);
        }
        [HttpGet("books/{id}/contents")]
        public ActionResult<IEnumerable<ContentModel>> GetBookContents(int id, int pageNo)
        {
            var contents = _efCore.GetBookContents(id, pageNo);
            return Ok(contents);
        }
        [HttpPost("books/{id}/contents")]
        public IActionResult CreateBookContent(ContentModel createModel)
        {
            var result = _efCore.CreateContent(createModel);
            var msg = result > 1 ? "Create success" : "Create fail";
            return Ok(msg);
        }
        [HttpPut("books/{id}/contents/{contentId}")]
        public IActionResult UpdateBookContent(int contentId, ContentModel updateModel)
        {
            var content = _efCore.GetCotent(contentId);
            if (content is null)
            {
                return NotFound("No record found.");
            }
            var result = _efCore.UpdateContent(contentId, updateModel);
            var msg = result > 1 ? "Update success" : "Update fail";
            return Ok(msg);
        }
        [HttpDelete("books/{id}/contents/{contentId}")]
        public IActionResult DeleteBookContent(int contentId)
        {
            var content = _efCore.GetCotent(contentId);
            if (content is null)
            {
                return NotFound("No record found.");
            }
            var result = _efCore.DeleteContent(content);
            var msg = result > 1 ? "Update success" : "Update fail";
            return Ok(msg);
        }

    }
}
