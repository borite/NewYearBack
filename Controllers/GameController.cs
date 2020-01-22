using ChinaAudio.Class;
using ChinaAudio.Classes.CodeHeper;
using Newtonsoft.Json;
using NewYear2020.DTO;
using NewYear2020.Entity;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Transactions;
using System.Web.Http;
using System.Web.Script.Serialization;
using static ChinaAudio.Class.Common;

namespace NewYear2020.Controllers
{
    /// <summary>
    /// 游戏数据调取API
    /// </summary>
    [RoutePrefix("api/Game")]
    public class GameController : ApiController
    {
        Common common = new Common();
        // CodeNew code = new CodeNew();
        YL_NewYear2020Entities NY = new YL_NewYear2020Entities();

        /// <summary>
        /// 获取成语游戏数据的选项
        /// </summary>
        /// <returns></returns>

        [HttpGet, Route("GetQuestion")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "code返回问题 message答案 data错误答案字符串数组")]
        public IHttpActionResult GetQuestion()
        {
            var getval = NY.Exam.OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();

            var aa = getval.Answer; //答案
            string textval = "一乙二十丁厂七卜人入八九几儿了力乃刀又三于干亏士工土才寸下大丈与万上小口巾山千乞川亿个勺久凡及夕丸么广亡门义之尸弓己已子卫也女飞刃习叉马乡丰王井开夫天无元专云扎艺木五支厅不太犬区历尤友匹车巨牙屯比互切瓦止少日中冈贝内水见午牛手毛气升长仁什片仆化仇币仍仅斤爪反介父从今凶分乏公仓月氏勿欠风丹匀乌凤勾文六方火为斗忆订计户认心尺引丑巴孔队办以允予劝双书幻玉刊示末未击打巧正扑扒功扔去甘世古节本术可丙左厉右石布龙平灭轧东卡北占业旧帅归且旦目叶甲申叮电号田由史只央兄叼叫另叨叹四生失禾丘付仗代仙们仪白仔他斥瓜乎丛令用甩印乐句匆册犯外处冬鸟务包饥主市立闪兰半汁汇头汉宁穴它讨写让礼训必议讯记永司尼民出辽奶奴加召皮边发孕圣对台矛纠母幼丝式刑动扛寺吉扣考托老执巩圾扩扫地扬场耳共芒亚芝朽朴机权过臣再协西压厌在有百存而页匠夸夺灰达列死成夹轨邪划迈毕至此贞师尘尖劣光当早吐吓虫曲团同吊吃因吸吗屿帆岁回岂刚则肉网年朱先丢舌竹迁乔伟传乒乓休伍伏优伐延件任伤价份华仰仿伙伪自血向似后行舟全会杀合兆企众爷伞创肌朵杂危旬旨负各名多争色壮冲冰庄庆亦刘齐交次衣产决充妄闭问闯羊并关米灯州汗污江池汤忙兴宇守宅字安讲军许论农讽设访寻那迅尽导异孙阵阳收阶阴防奸如妇好她妈戏羽观欢买红纤级约纪驰巡寿弄麦形进戒吞远违运扶抚坛技坏扰拒找批扯址走抄坝贡攻赤折抓扮抢孝均抛投坟抗坑坊抖护壳志扭块声把报却劫芽花芹芬苍芳严芦劳克苏杆杠杜材村杏极李杨求更束豆两丽医辰励否还歼来连步坚旱盯呈时吴助县里呆园旷围呀吨足邮男困吵串员听吩吹呜吧吼别岗帐财针钉告我乱利秃秀私每兵估体何但伸作伯伶佣低你住位伴身皂佛近彻役返余希坐谷妥含邻岔肝肚肠龟免狂犹角删条卵岛迎饭饮系言冻状亩况床库疗应冷这序辛弃冶忘闲间闷判灶灿弟汪沙汽沃泛沟没沈沉怀忧快完宋宏牢究穷灾良证启评补初社识诉诊词译君灵即层尿尾迟局改张忌际陆阿陈阻附妙妖妨努忍劲鸡驱纯纱纳纲驳纵纷纸纹纺驴纽奉玩环武青责现表规抹拢拔拣担坦押抽拐拖拍者顶拆拥抵拘势抱垃拉拦拌幸招坡披拨择抬其取苦若茂苹苗英范直茄茎茅林枝杯柜析板松枪构杰述枕丧或画卧事刺枣雨卖矿码厕奔奇奋态欧垄妻轰顷转斩轮软到非叔肯齿些虎虏肾贤尚旺具果味昆国昌畅明易昂典固忠咐呼鸣咏呢岸岩帖罗帜岭凯败贩购图钓制知垂牧物乖刮秆和季委佳侍供使例版侄侦侧凭侨佩货依的迫质欣征往爬彼径所舍金命斧爸采受乳贪念贫肤肺肢肿胀朋股肥服胁周昏鱼兔狐忽狗备饰饱饲变京享店夜庙府底剂郊废净盲放刻育闸闹郑券卷单炒炊炕炎炉沫浅法泄河沾泪油泊沿泡注泻泳泥沸波泼泽治怖性怕怜怪学宝宗定宜审宙官空帘实试郎诗肩房诚衬衫视话诞询该详建肃录隶居届刷屈弦承孟孤陕降限妹姑姐姓始驾参艰线练组细驶织终驻驼绍经贯奏春帮珍玻毒型挂封持项垮挎城挠政赴赵挡挺括拴拾挑指垫挣挤拼挖按挥挪某甚革荐巷带草茧茶荒茫荡荣故胡南药标枯柄栋相查柏柳柱柿栏树要咸威歪研砖厘厚砌砍面耐耍牵残殃轻鸦皆背战点临览竖省削尝是盼眨哄显哑冒映星昨畏趴胃贵界虹虾蚁思蚂虽品咽骂哗咱响哈咬咳哪炭峡罚贱贴骨钞钟钢钥钩卸缸拜看矩怎牲选适秒香种秋科重复竿段便俩贷顺修保促侮俭俗俘信皇泉鬼侵追俊盾待律很须叙剑逃食盆胆胜胞胖脉勉狭狮独狡狱狠贸怨急饶蚀饺饼弯将奖哀亭亮度迹庭疮疯疫疤姿亲音帝施闻阀阁差养美姜叛送类迷前首逆总炼炸炮烂剃洁洪洒浇浊洞测洗活派洽染济洋洲浑浓津恒恢恰恼恨举觉宣室宫宪突穿窃客冠语扁袄祖神祝误诱说诵垦退既屋昼费陡眉孩除险院娃姥姨姻娇怒架贺盈勇怠柔垒绑绒结绕骄绘给络骆绝绞统耕耗艳泰珠班素蚕顽盏匪捞栽捕振载赶起盐捎捏埋捉捆捐损都哲逝捡换挽热恐壶挨耻耽恭莲莫荷获晋恶真框桂档桐株桥桃格校核样根索哥速逗栗配翅辱唇夏础破原套逐烈殊顾轿较顿毙致柴桌虑监紧党晒眠晓鸭晃晌晕蚊哨哭恩唤啊唉罢峰圆贼贿钱钳钻铁铃铅缺氧特牺造乘敌秤租积秧秩称秘透笔笑笋债借值倚倾倒倘俱倡候俯倍倦健臭射躬息徒徐舰舱般航途拿爹爱颂翁脆脂胸胳脏胶脑狸狼逢留皱饿恋桨浆衰高席准座脊症病疾疼疲效离唐资凉站剖竞部旁旅畜阅羞瓶拳粉料益兼烤烘烦烧烛烟递涛浙涝酒涉消浩海涂浴浮流润浪浸涨烫涌悟悄悔悦害宽家宵宴宾窄容宰案请朗诸读扇袜袖袍被祥课谁调冤谅谈谊剥恳展剧屑弱陵陶陷陪娱娘通能难预桑绢绣验继球理捧堵描域掩捷排掉堆推掀授教掏掠培接控探据掘职基著勒黄萌萝菌菜萄菊萍菠营械梦梢梅检梳梯桶救副票戚爽聋袭盛雪辅辆虚雀堂常匙晨睁眯眼悬野啦晚啄距跃略蛇累唱患唯崖崭崇圈铜铲银甜梨犁移笨笼笛符第敏做袋悠偿偶偷您售停偏假得衔盘船斜盒鸽悉欲彩领脚脖脸脱象够猜猪猎猫猛馅馆凑减毫麻痒痕廊康庸鹿盗章竟商族旋望率着盖粘粗粒断剪兽清添淋淹渠渐混渔淘液淡深婆梁渗情惜惭悼惧惕惊惨惯寇寄宿窑密谋谎祸谜逮敢屠弹随蛋隆隐婚婶颈绩绪续骑绳维绵绸绿琴斑替款堪搭塔越趁趋超提堤博揭喜插揪搜煮援裁搁搂搅握揉斯期欺联散惹葬葛董葡敬葱落朝辜葵棒棋植森椅椒棵棍棉棚棕惠惑逼厨厦硬确雁殖裂雄暂雅辈悲紫辉敞赏掌晴暑最量喷晶喇遇喊景践跌跑遗蛙蛛蜓喝喂喘喉幅帽赌赔黑铸铺链销锁锄锅锈锋锐短智毯鹅剩稍程稀税筐等筑策筛筒答筋筝傲傅牌堡集焦傍储奥街惩御循艇舒番释禽腊脾腔鲁猾猴然馋装蛮就痛童阔善羡普粪尊道曾焰港湖渣湿温渴滑湾渡游滋溉愤慌惰愧愉慨割寒富窜窝窗遍裕裤裙谢谣谦属屡强粥疏隔隙絮嫂登缎缓编骗缘瑞魂肆摄摸填搏塌鼓摆携搬摇搞塘摊蒜勤鹊蓝墓幕蓬蓄蒙蒸献禁楚想槐榆楼概赖酬感碍碑碎碰碗碌雷零雾雹输督龄鉴睛睡睬鄙愚暖盟歇暗照跨跳跪路跟遣蛾蜂嗓置罪罩错锡锣锤锦键锯矮辞稠愁筹签简毁舅鼠催傻像躲微愈遥腰腥腹腾腿触解酱痰廉新韵意粮数煎塑慈煤煌满漠源滤滥滔溪溜滚滨粱滩慎誉塞谨福群殿辟障嫌嫁叠缝缠静碧璃墙撇嘉摧截誓境摘摔聚蔽慕暮蔑模榴榜榨歌遭酷酿酸磁愿需弊裳颗嗽蜻蜡蝇蜘赚锹锻舞稳算箩管僚鼻魄貌膜膊膀鲜疑馒裹敲豪膏遮腐瘦辣竭端旗精歉熄熔漆漂漫滴演漏慢寨赛察蜜谱嫩翠熊凳骡缩慧撕撒趣趟撑播撞撤增聪鞋蕉蔬横槽樱橡飘醋醉震霉瞒题暴瞎影踢踏踩踪蝶蝴嘱墨镇靠稻黎稿稼箱箭篇僵躺僻德艘膝膛熟摩颜毅糊遵潜潮懂额慰劈操燕薯薪薄颠橘整融醒餐嘴蹄器赠默镜赞篮邀衡膨雕磨凝辨辩糖糕燃澡激懒壁避缴戴擦鞠藏霜霞瞧蹈螺穗繁辫赢糟糠燥臂翼骤鞭覆蹦镰翻鹰警攀蹲颤瓣爆疆壤耀躁嚼嚷籍魔灌蠢霸露囊罐翎";
            char[] stringArray = textval.ToCharArray(); //错误字体
            string[] IMGlist = Array.Empty<string>(); //返回一个空数组
            List<string> StringList = IMGlist.ToList();//后面可以添加
            Random rnd = new Random();
            //随机抽取17个
            for (int i = 0; i < 100; i++)
            {
                int num = rnd.Next(2501);
                if (stringArray[num].ToString() == aa) //与正确答案冲突，进入下一次循环
                {
                    continue;
                }
                else//与正确答案不冲突
                {
                    if (StringList.Count == 17)
                    {
                        break;
                    }
                    else//没到17次，继续添加
                    {
                        StringList.Add(stringArray[num].ToString());
                    }
                }
            }



            return Content(HttpStatusCode.OK, CodeNew.Success(getval.Question, getval.Answer.Trim(), StringList));

        }
        /// <summary>
        /// 用户验证（判断该微信号是否注册过）
        /// </summary>
        /// <param name="Code">传入code获取openID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckOpenID")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "200验证成功，返回个人信息数据 201新用户，请跳转到注册页，openID在data下 ，400获取openID失败可能是code不正确或者过期", Type = typeof(UserInfoReturnDTO))]
        public IHttpActionResult CheckOpenID([FromUri] string Code)
        {

            TokenMessage val = common.GetOpenIDFun(Code);
            if (val.openid == null)
            {
                return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.BadRequest, "code可能错误或者已经过期，没有获取openID", val.openid));

            }
            var isRegister = NY.User.Where(a => a.OpenID == val.openid).FirstOrDefault();
            if (isRegister == null) //该用户尚未注册，跳转到注册页
            {
                return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.Created, "新用户，请跳转注册页", val));
            }
            else
            {


                return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.OK, "验证成功，返回个人信息", isRegister));


            }






        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("Register")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "200注册成功 400验证错误，具体错误信息见message", Type = typeof(UserInfoReturnDTO))]
        public IHttpActionResult Register(UserRegisterDTO obj)
        {
            var empolyCheck = NY.Employing.Where(a => a.JobID == obj.JobID && a.UserName == obj.Name).FirstOrDefault();
            if (empolyCheck == null) //如果没有找到该信息
            {



                return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.BadRequest, "验证失败，没有找到对应的工号及姓名，请输入正确的工号、姓名", ""));


            }
            else
            {
                var user = NY.User.Where(a => a.OpenID == obj.OpenID || a.JobID == obj.JobID).FirstOrDefault();
                if (user != null) //如果有冲突
                {
                    if (user.OpenID == obj.OpenID)
                    {
                        return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.BadRequest, "验证失败，该微信号（openID）已注册", ""));

                    }
                    else
                    {
                        return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.BadRequest, "验证失败，该工号已经注册,请勿重复注册", ""));
                    }


                }
                else //注册
                {
                    // var usersearch = NY.Employing.
                    User res = new User();
                    res.Name = obj.Name;
                    res.Phone = obj.Phone;
                    res.JobID = obj.JobID;
                    res.OpenID = obj.OpenID;
                    res.WeChatNickName = obj.WeChatNickName;
                    res.HeadIMG = obj.HeadIMG;
                    res.Section = empolyCheck.Section;
                    res.Department = empolyCheck.Department;
                  //  res.   empolyCheck
                    NY.User.Add(res);
                    NY.SaveChanges();
                    return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.OK, "注册成功，返回个人信息", res));

                    // res.JobID = obj.JobID;
                }
            }






        }

        /// <summary>
        /// 通过OpenID获取个人信息
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        [HttpGet, Route("GetInfo")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "200获取数据成功", Type = typeof(UserInfoReturnDTO))]
        public IHttpActionResult GetInfo([FromUri] string OpenID)
        {
            var CC = NY.User.Where(A => A.OpenID == OpenID).FirstOrDefault();
            return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.OK, "返回个人信息成功", CC));

        }

        /// <summary>
        /// 随机抽取一张卡片
        /// </summary>
        /// <param name="openID">微信openid</param>
        /// <param name="GameType">游戏参数分别是1.2.3.4.5（代表5个游戏）</param>
        /// <returns></returns>
        [HttpGet, Route("ChouCard")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "200获取数据成功 204当日抽奖两次已经用完，请明天再抽，400服务器繁忙，请重新抽取，404游戏时间已过，不能抽牌")]

        //public IHttpActionResult UpdateUserCardNum(string openid, int type)
        public IHttpActionResult ChouCard(string openID, int GameType)
        {
            ////五天日期
            //DateTime end1 = new DateTime(2020, 1, 18);
            //DateTime end21 = new DateTime(2020, 1, 19);
            //DateTime end22 = new DateTime(2020, 1, 20);
            //DateTime end23 = new DateTime(2020, 1, 23);
            //DateTime end24 = new DateTime(2020, 1, 24);
            //时间判断布尔判断是否是今天
            var manager = TimeManager();
            bool day20 = manager.day20;
            bool day21 = manager.day21;
            bool day22 = manager.day22;
            bool day23 = manager.day23;
            bool day24 = manager.day24;
            //数量判断
          //  int num20 = manager.day20; 
            int num20 = CardManager.num20;
            int num21 = CardManager.num21;
            int num22 = CardManager.num22;
            int num23 = CardManager.num23;
            int num24 = CardManager.num24;

            //每天抽几次
            DateTime nowtimeDate = DateTime.Now;
            int rate = 2;


            //查找当天的抽奖次数 如果当天
            //var userChou = NY.TimeRecord.Where(a => a.OpenID == openID && EntityFunctions.TruncateTime(a.AddTime) == EntityFunctions.TruncateTime(end1)).ToList();
            //当天该用户的抽卡情况
            var userChou = NY.TimeRecord.Where(a => a.OpenID == openID && DbFunctions.DiffDays(a.AddTime, nowtimeDate) < 1 && a.GameType == GameType).ToList();
            if (userChou.Count < rate)
            {
                if (day20) //20日
                {
                    //随机查询一个大于8000的卡片数量(20日规定的指定数量)
                    var cc = NY.CardList.Where(a => a.Total >= num20).OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();

                    var res = common.CardManagerFun(openID, cc.ID);
                    if (res == "success") //成功
                    {
                        //增加时间记录
                        TimeRecord addval = new TimeRecord();
                        addval.OpenID = openID;
                        addval.AddTime = nowtimeDate;
                        addval.GameType = GameType;
                        NY.TimeRecord.Add(addval);
                        NY.SaveChanges();



                        return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.OK, "随机抽取一张卡片", cc.CardName.Trim()));

                    }
                    else// (res== "null"||res== "busy")
                    {
                        return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.BadRequest, "服务器繁忙，请重新尝试（可能遇到了并发或者事务回滚）", ""));

                    }

                }

                if (day21) //21日
                {
                    //随机查询一个大于8000的卡片数量(20日规定的指定数量)
                    var cc = NY.CardList.Where(a => a.Total >= num21).OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();

                    var res = common.CardManagerFun(openID, cc.ID);
                    if (res == "success") //成功
                    {
                        //增加时间记录
                        TimeRecord addval = new TimeRecord();
                        addval.OpenID = openID;
                        addval.AddTime = nowtimeDate;
                        addval.GameType = GameType;
                        NY.TimeRecord.Add(addval);
                        NY.SaveChanges();



                        return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.OK, "随机抽取一张卡片", cc.CardName.Trim()));

                    }
                    else// (res== "null"||res== "busy")
                    {
                        return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.BadRequest, "服务器繁忙，请重新尝试（可能遇到了并发或者事务回滚）", ""));

                    }

                }
                if (day22) //21日
                {
                    //随机查询一个大于8000的卡片数量(20日规定的指定数量)
                    var cc = NY.CardList.Where(a => a.Total >= num22).OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();

                    var res = common.CardManagerFun(openID, cc.ID);
                    if (res == "success") //成功
                    {
                        //增加时间记录
                        TimeRecord addval = new TimeRecord();
                        addval.OpenID = openID;
                        addval.AddTime = nowtimeDate;
                        addval.GameType = GameType;
                        NY.TimeRecord.Add(addval);
                        NY.SaveChanges();



                        return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.OK, "随机抽取一张卡片", cc.CardName.Trim()));

                    }
                    else// (res== "null"||res== "busy")
                    {
                        return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.BadRequest, "服务器繁忙，请重新尝试（可能遇到了并发或者事务回滚）", ""));

                    }

                }
                if (day23) //21日
                {
                    //随机查询一个大于8000的卡片数量(20日规定的指定数量)
                    var cc = NY.CardList.Where(a => a.Total >= num23).OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();

                    var res = common.CardManagerFun(openID, cc.ID);
                    if (res == "success") //成功
                    {
                        //增加时间记录
                        TimeRecord addval = new TimeRecord();
                        addval.OpenID = openID;
                        addval.AddTime = nowtimeDate;
                        addval.GameType = GameType;
                        NY.TimeRecord.Add(addval);
                        NY.SaveChanges();



                        return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.OK, "随机抽取一张卡片", cc.CardName.Trim()));

                    }
                    else// (res== "null"||res== "busy")
                    {
                        return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.BadRequest, "服务器繁忙，请重新尝试（可能遇到了并发或者事务回滚）", ""));

                    }

                }
                if (day24) //21日
                {
                    //随机查询一个大于8000的卡片数量(20日规定的指定数量)
                    var cc = NY.CardList.Where(a => a.Total > num24).OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();

                    var res = common.CardManagerFun(openID, cc.ID);
                    if (res == "success") //成功
                    {
                        //增加时间记录
                        TimeRecord addval = new TimeRecord();
                        addval.OpenID = openID;
                        addval.AddTime = nowtimeDate;
                        addval.GameType = GameType;
                        NY.TimeRecord.Add(addval);
                        NY.SaveChanges();



                        return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.OK, "随机抽取一张卡片", cc.CardName.Trim()));

                    }
                    else// (res== "null"||res== "busy")
                    {
                        return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.BadRequest, "服务器繁忙，请重新尝试（可能遇到了并发或者事务回滚）", ""));

                    }

                }
                else
                {
                    return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.NotFound, "游戏活动时间已经结束 ,不能抽牌", ""));
                }

            }
            else
            {
                return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.NoContent, "今日两次抽卡已经", ""));

            }


        }




        /// <summary>
        /// 进行一次抽奖
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        [HttpGet, Route("ChouPrize")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "200获取数据成功 204逻辑问题出现问题，具体见message描述问题，400服务器繁忙，请重新抽取，404游戏时间已过，不能抽牌")]

        public IHttpActionResult GetChou(string OpenID)
        {

            var cc = NY.User.Where(a => a.OpenID == OpenID && a.ISChou == false).FirstOrDefault();
            if (cc == null) //该用户已经抽过奖，或者openID不正确
            {

                return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.NoContent, "该用户已经抽过奖了吧？抽过就不能再抽了！要不就是openID错了", ""));
            }
            else //未抽奖
            {
                //  启用事务
                using (TransactionScope ts = new TransactionScope())
                {



                    if (cc.c1snf == 0 || cc.c2snm == 0 || cc.c3snq == 0 || cc.c4snl == 0 || cc.c5snt == 0) //卡片还没集齐就抽奖？
                    {
                        return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.NoContent, "卡片还没集齐就抽奖？先判断5张卡片集齐了再访问这个API", ""));
                    }
                    else  //集齐5张卡片，开始抽奖
                    {
                        var chou = NY.PrizeList.Where(a => a.PrizeCount > 0).OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();
                        chou.PrizeCount -= 1; //清除库存
                        NY.Entry(chou).Property("PrizeCount").IsModified = true;
                        if (chou.ID == 9) //如果是爱奇艺
                        {
                            //爱奇艺表清库存 状态更改
                            var aiqiyi = NY.AiQiYi.Where(a => a.OpenID == null && a.ChouTime == null).FirstOrDefault();
                            aiqiyi.ChouTime = DateTime.Now; //抽奖时间赋值
                            aiqiyi.OpenID = cc.OpenID; //openID拥有者赋值
                            NY.Entry(aiqiyi).Property("ChouTime").IsModified = true;
                            NY.Entry(aiqiyi).Property("OpenID").IsModified = true;

                            //用户表赋值
                            cc.aiqiyiKey = aiqiyi.Keys;
                            NY.Entry(cc).Property("aiqiyiKey").IsModified = true;


                        }
                        cc.c1snf -= 1;
                        cc.c2snm -= 1;
                        cc.c3snq -= 1;
                        cc.c4snl -= 1;
                        cc.c5snt -= 1;
                       
                        cc.PrizeID = chou.ID; //用户礼品赋值
                        cc.ISChou = true;  //抽奖状态更改                   
                        NY.Entry(cc).Property("PrizeID").IsModified = true;
                        NY.Entry(cc).Property("ISChou").IsModified = true;
                        NY.Entry(cc).Property("c1snf").IsModified = true;
                        NY.Entry(cc).Property("c2snm").IsModified = true;
                        NY.Entry(cc).Property("c3snq").IsModified = true;
                        NY.Entry(cc).Property("c4snl").IsModified = true;
                        NY.Entry(cc).Property("c5snt").IsModified = true;

                        try
                        {
                            NY.SaveChanges();
                            ts.Complete();
                            return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.OK, chou.PrizeName, cc.aiqiyiKey));
                        }
                        catch (Exception ex)
                        {

                            return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.BadRequest, "服务器繁忙请重新点击", ""));
                        }



                    }




                }


            }





        }


      

        /// <summary>
        /// 尊重卡随机抽一张卡
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>

        [HttpGet,Route("UseZZK")]
        public IHttpActionResult UseZZK([FromUri] string OpenID)
        {
            var user = NY.User.Where(a => a.OpenID == OpenID && a.c6zzk > 0).FirstOrDefault();
            if (user==null)
            {
                return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.BadRequest, "API错误，卡片没了就不能再换了，前端逻辑出问题了吧", ""));

            }


            user.c6zzk -= 1;
          //  common.CardFun(user.OpenID);
            NY.SaveChanges();
           // return Content(common.CardFun(user.OpenID));
            return Content(HttpStatusCode.OK, common.CardFun(user.OpenID));

        }


        
        /// <summary>
        /// 用户送一张卡
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost, Route("GiveCard")]
        public IHttpActionResult GivenCard(GivenCardDTO obj)
        {
            try
            {
                GiveCardRecord res = new GiveCardRecord();
                if (obj.CardID == 1) //数你富
                {
                    var user = NY.User.Where(a => a.OpenID == obj. OpenID && a.c1snf > 0).FirstOrDefault();
                    var Touser = NY.User.Where(a => a.OpenID == obj.toOpenID.Trim()).FirstOrDefault();
                    user.c1snf -= 1;
                    Touser.c1snf += 1;
                 
                    res.CardID = obj.CardID;
                    res.OpenID = obj.OpenID;
                    res.ToOpenID = obj.toOpenID.Trim();
                    res.GiveTime = DateTime.Now;
                    res.UserName = user.Name;
                   

                }
                if (obj. CardID == 2) //鼠你美
                {
                    var user = NY.User.Where(a => a.OpenID == obj.OpenID && a.c2snm > 0).FirstOrDefault();
                    var Touser = NY.User.Where(a => a.OpenID == obj.toOpenID.Trim()).FirstOrDefault();
                    user.c2snm -= 1;
                    Touser.c2snm += 1;
                  
                    res.CardID =obj. CardID;
                    res.OpenID = obj. OpenID;
                    res.ToOpenID = obj. toOpenID.Trim();
                    res.GiveTime = DateTime.Now;
                    res.UserName = user.Name;

                }
                if (obj. CardID == 4) //鼠你强
                {
                    var user = NY.User.Where(a => a.OpenID == obj.OpenID && a.c3snq > 0).FirstOrDefault();
                    var Touser = NY.User.Where(a => a.OpenID ==obj. toOpenID.Trim()).FirstOrDefault();
                    user.c3snq -= 1;
                    Touser.c3snq += 1;
                   
                    res.CardID = obj.CardID;
                    res.OpenID = obj.OpenID;
                    res.ToOpenID =obj. toOpenID.Trim();
                    res.GiveTime = DateTime.Now;
                    res.UserName = user.Name;

                }
                if (obj.CardID == 5) //鼠你甜
                {
                    var user = NY.User.Where(a => a.OpenID == obj.OpenID && a.c5snt > 0).FirstOrDefault();
                    var Touser = NY.User.Where(a => a.OpenID == obj.toOpenID.Trim()).FirstOrDefault();
                    user.c5snt -= 1;
                    Touser.c5snt += 1;
                 
                    res.CardID = obj.CardID;
                    res.OpenID = obj.OpenID;
                    res.ToOpenID = obj.toOpenID.Trim();
                    res.GiveTime = DateTime.Now;
                    res.UserName = user.Name;

                }
                if (obj.CardID == 6) //鼠你乐
                {
                    var user = NY.User.Where(a => a.OpenID == obj.OpenID && a.c4snl > 0).FirstOrDefault();
                    var Touser = NY.User.Where(a => a.OpenID == obj.toOpenID.Trim()).FirstOrDefault();
                    user.c4snl -= 1;
                    Touser.c4snl += 1;
                
                    res.CardID = obj.CardID;
                    res.OpenID = obj.OpenID;
                    res.ToOpenID = obj.toOpenID.Trim();
                    res.GiveTime = DateTime.Now;
                    res.UserName = user.Name;

                }
                if (obj.CardID == 7)
                {
                    var user = NY.User.Where(a => a.OpenID == obj.OpenID && a.c6zzk > 0).FirstOrDefault();
                    var Touser = NY.User.Where(a => a.OpenID == obj.toOpenID.Trim()).FirstOrDefault();
                    user.c6zzk -= 1;
                    Touser.c6zzk += 1;
                   
                    res.CardID = obj.CardID;
                    res.OpenID = obj.OpenID;
                    res.ToOpenID = obj.toOpenID.Trim();
                    res.GiveTime = DateTime.Now;
                    res.UserName = user.Name;



                }

                NY.GiveCardRecord.Add(res);


                NY.SaveChanges();
                return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.OK, "卡片赠送成功", ""));

            }
            catch (Exception e)
            {

                return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.BadRequest, "失败，问题见data", e));
             
            }

           






        }


        /// <summary>
        /// 用户收到赠送卡片列表
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>

        [HttpGet,Route("GetUserGivenList")]
        public IHttpActionResult GetUserGivenList([FromUri] string OpenID )
        {

            var list = NY.GiveCardRecord.Where(a => a.ToOpenID == OpenID).Select(a => new
            GetCardListDTO
            {
                ID=a.ID,
              //  OpenID=a.OpenID,
              
                GiveTime=a.GiveTime,
                UserName=a.UserName,
               // CardID=a.CardID,
                CardName=a.CardList.CardName
                

            
            }
            ).OrderByDescending(a => a.GiveTime).ThenByDescending(a=>a.ID).ToList();




            return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.OK, "用户收到赠送卡片列表",list));

        }

        /// <summary>
        /// 查找要赠送用户的OpenID（赠送时需要这个API获取目标用户）
        /// </summary>
        /// <param name="Name">名字</param>
        /// <param name="JObID">工号</param>
        /// <returns></returns>
        [HttpGet,Route("SearchUsers")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "200获取数据成功 404 找不到该用户可能该用户并没有参与游戏")]

        public IHttpActionResult SearchUsers([FromUri] string Name ,string JObID)
        {
            var cc = NY.User.Where(a => a.JobID == JObID && a.Name == Name).FirstOrDefault();
            if (cc==null)
            {
                return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.NotFound, "没有找到该用户，可能该用户并没有参与游戏", ""));

            }
            else
            {
                return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.OK, "找到用户返回ID", cc.OpenID));
            }

  
        }



        /// <summary>
        /// 中奖后用户输入地址API
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut, Route("UpDateAddress")]
        public IHttpActionResult UpDateAddress(AddressDTO obj)
        {

            var user = NY.User.Where(a => a.OpenID == obj.OpenID).FirstOrDefault();

            user.Address = obj.Address;
            NY.SaveChanges();
          return Content(HttpStatusCode.OK, CodeNew.Success(HttpStatusCode.OK, "成功录入地址", ""));




        }







    }
}
