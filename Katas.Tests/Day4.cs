﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Katas.Tests
{
    [TestFixture]
    public class Day4
    {
        private uint Solve(IEnumerable<string> phrases)
        {
            var list = phrases.ToList();
            Console.WriteLine($"Phrases Input: {list.Count}");

            var validCount = 0;

            var regex = new Regex(@"\w+", RegexOptions.IgnoreCase);

            foreach (var phrase in list)
            {
                var words = regex.Matches(phrase).Cast<Match>().ToList();
                var alphaSortedWords = words
                    .Select(m => string.Concat(m.Value.OrderBy(c => c)))
                    .Distinct()
                    .Count();


                if (words.Count == alphaSortedWords)
                {
                    Console.WriteLine($"Phrase Valid: '{phrase}' - Words: {words.Count}, Unique: {alphaSortedWords}");
                    validCount++;
                }
            }

            return (uint)validCount;
        }

        [TestCase("abcde fghij", 1)]
        [TestCase("abcde xyz ecdab", 0)]
        [TestCase("a ab abc abd abf abj", 1)]
        [TestCase("iiii oiii ooii oooi oooo\roiii ioii iioi iiio", 1)]
        public void Solve_SamplePhrases_Returns_Expected(string phrases, int expectedValidCount)
        {
            var solution = Solve(phrases.Split('\r'));

            Assert.That(solution, Is.EqualTo(expectedValidCount));
        }

        [Test]
        //[Ignore("WIP")]
        public void Solve_ChallengeInput_Produces_WinningResult()
        {
            var solution = Solve("vxjtwn vjnxtw sxibvv mmws wjvtxn icawnd rprh[NL]fhaa qwy vqbq gsswej lxr yzl wakcige mwjrl[NL]bhnlow huqa gtbjc gvj wrkyr jgvmhj bgs umo ikbpdto[NL]drczdf bglmf gsx flcf ojpj kzrwrho owbkl dgrnv bggjevc[NL]ndncqdl lncaugj mfa lncaugj skt pkssyen rsb npjzf[NL]kdd itdyhe pvljizn cgi[NL]jgy pyhuq eecb phwkyl oeftyu pyhuq hecxgti tpadffm jgy[NL]zvc qdk mlmyj kybbh lgbb fvfzcer frmaxa yzgw podt dbycoii afj[NL]zfr msn mns leqem frz[NL]golnm ltizhd dvwv xrizqhd omegnez nan yqajse lgef[NL]gbej rvek aehiz bgje[NL]yej cphl jtp swe axhljo ddwk obwsq mnewiwu klddd[NL]ipiev henods rpn qfpg gjfdgs zcpt sswab eosdhn teeil[NL]gzje ydu oiu jzge udy sqjeoo olxej[NL]mgn gox tcifta vzc lxry gox gox mvila qdl jipjnw dvu[NL]hxk xhk unhdmdz yomze povrt nbww bxu qqsqc rvuk tgffy twddm[NL]fyx fyx nzkm fyx[NL]ymnoc zogudq yncom tqrob sidvy dfuu ccjpiej yidvs[NL]bxebny akknwxw jeyxqvj syl cedps akknwxw akknwxw zpvnf kuoon pnkejn wqjgc[NL]kcebrkj zmuf ueewxhi mgyepbr nleviqc dez[NL]argavx fqguii gebohvw klnrq rkqnl goevhbw[NL]ywqi abwi eswph nlplbl pswhe lnqx fpgk lllnpb[NL]abpb mpkw ampey yapme xnuyj[NL]tmuaq asd bhbs sqmbsnw wsbnqsm ydwdncn rpa vrllkh[NL]dnltf cck djy ydj[NL]wywwl scezo clowuz dkgqaj dohyzcp[NL]diimshr vlmsnlj whqb dkicau ckdaiu terp kgcii npqc vvzrqzv nol[NL]wfpxe sqf tbb ruqpcq zfgb[NL]kajykuz tsxgtys vuz kglmgg ihnnn plyjxj rcrvo mij plyjxj jqiur[NL]pxs hmet dwgvd mvhkvn cjxg yvid vmhnkv kwxz rfemsua wdgvd okixk[NL]lzwxas ddtyeh ivyama crrhxdt hedytd jfw[NL]vjfv oyd fvjv kfwlj mradbx mckseee xradmb[NL]llga yytxyvj lstspek lstspek lstspek[NL]fabgf wgop fabgf bvsfoaw[NL]grqnbvo tntomdw hizg tmotdwn[NL]mau ufkw cxfi rhehj ebe xyv rhehj acxngo arl qtl rhehj[NL]kbkto stqjtm tpcwshj saerkrt pffj dthp pfjf axc gwmmfdw glnqtdy xmskw[NL]veff zqm hzhxap lgwnwq twsdk mqz xbbarbv cdx fhnwt qjcji bbvbrxa[NL]fjw eds hofskl nkbsv des hvx xyn[NL]qzort qzort qesz rtq oonk vwzlw wapoj ifr cta[NL]pja hvy nhjg paj smtfe fmtse[NL]xvi tcjj xvkjtab nqftt aumijl xkd cmilegf hvsmodx uuo igmcelf mslkq[NL]mdhezgv lelzy kzfvsqu hvmvaxw pxiqjc hvmvaxw kzfvsqu[NL]hsicsav csshrhx znojm eapi lhmzq bbwnz seao gfk azk[NL]pup xtgjyzy wqt ijeektl[NL]ktwh qdegzs btj pfwzzho[NL]xdkmdm izqtjrr iqbke vtp[NL]fmrbpdr zpccv tmtwx tmtwx tmtwx bys[NL]ehphfgq idd ehphfgq ehphfgq uphe hvrc jcscne nbnslqy[NL]xzqucgj fcih fljk barz lvln hcfi azrb[NL]cmfmclv mfgvifw rnxgn jpg bsnq wnduzj ymsdx smdxy pqomf[NL]rlqsm qrsml emts qsmcowv scmvwqo[NL]tshzkpa zwtpda ftsiwo nil tpawdz kjpa ptzashk[NL]mnep sfc swjawtd vnwud gyulluw zpa kmwyvln evd btnmoi dnwe[NL]jwq scepq redoxmw rbdzsa wlkzso kxpm bttg vxuc moxwdre ijtdd rzsabd[NL]wpvo dsjox amuwjm pls lgwksva ctakgpl rmsjj lzwwpr zzm udg[NL]bji obbn tmwyc afpmkxr glvrd kahhgpq rna qkxyntp vmd mloshc[NL]ymq rtnr nxjzm pqiddrn qmy vgxw ull[NL]mmzk ikge zhtzhs xyo qwe lll gjjm icetq qgrr mzwqa knec[NL]kxomfck idlh xrbowo nyetbnl qskh xuwkkxe upmmmf zhvuyp[NL]srcwyhl czgr xmhuws jueyh xcuib xhsuwm bxuic[NL]crkueh beyxopz xpyozbe dxgadw qktmce rjropjg[NL]lbktun imdpcp fkssp fhcpt fehho jqdnt aoewa[NL]jmun pynzjo trs ijwcc pelf oft pcsqdxg zvql[NL]mneaaq vjrg jidlrzz phd mvxpivd ldkhu[NL]sao xqw nrukn gatkz quscpsx vmz oscoeb[NL]goi wzxhb rrk aylqqcd mlcbvvf ororn heptid kdu byevr[NL]qsj lsbieef deez vzwdx hez iwd[NL]lmgfb keqt mqbsuis ogrr errbi xiqe xsszacp[NL]ato hmk zfjaj kmh plxup cida dqd pfwh nkbxvpr buajw pxkrvnb[NL]cli bdwu vrwott vowtrt grle[NL]zisgks ciuaqr zvk tcb kvz ugmtv[NL]oegrojm wofpwp gnaocx rweyull ellhwow dtefylf dqsz oiw varr bcirpf oxusz[NL]oydkmib oydkmib yefts gbl gbl[NL]sruwjk pgkrp kea gppkr zdcky cfljh[NL]obpxbax jhpcrj slcsa lgd fborz vvpaus wsrpsws ifijuzo[NL]rixz jwh uhdaf hoacv hdfua[NL]kntk qprmfow kntk tbmcjx[NL]vnqe ooyxtb ixl hdmnpn orpz ykspl xromvj kowtq wmho gquos[NL]ynk xjjqw sut lmtub bmtlu zdo dztlk bpkuul smhpx rbczg[NL]zals csdbe sbj dibicq kdfwwt[NL]coyy pjddlfc lwvhyms ldjdcfp ryubz kfwst dqjrjja jtv jjjaqrd[NL]jaexhms iqoiln ewgyr exmnrr fsr lgmyy fdofhn[NL]pjgyn hfoz zbcnz nczbz[NL]ovntivq vcey vdrkse giu ohyaxy ionyy fvpn yvwrgrv qta[NL]yelpz htbk njgeyub tggh mdthzp fwyux rduqli twlhfp pnh gywif ttn[NL]yxhsbil vplsmmx rgtq grsf lyibxhs hctnkfr awg lmloz jroy lpgb wga[NL]kzytass szyksat tyskasz ehmhhu[NL]jkus hwjv ymnnkk yffugg cvtnits gbl lywkn szihcn dbrbalf rxqpbqh[NL]koyfcef wkom mwok qgjrytl[NL]slmhry lcr slmhry lcr[NL]mvoxbt cfkz purnsui xar ouhtc thbx[NL]xcdifw kvvxyrj knac qmypw bou tmukqy eusgaoo bktiu[NL]ablgnhb axumg bwpxnjp zqpc vtw ghhoxu zqpc znfpvl ghhoxu jlg ntdk[NL]vmvc cdkhrx cvz rvxk mmcuo udpcayd lsmm gufduzt linj[NL]mgyeqkv hqionh rgnqgz kkc qrgnzg egkmqyv topdp[NL]koa dimwx gjxa atlfdy[NL]uuez ueuz zeuu ezo daq[NL]ofpaw bgomvmt mqa dexpy mbipd epyzcoa nuwrh vwly xppz qkjrleo rwhnu[NL]wok grxk lchvtg plrzr lwaax cfeu ijapws dmkdwc cfeu[NL]zkd hysxxip hlydw wicsvy gbwoaw dapre ktjn dzg uri[NL]blzh hblz qgmjceg fyf[NL]vkhpn xnc ogva pjrh cxn hkpnv[NL]aja cldzta tdcazl lorr fwmxxh knilf ges tdhp gnlo vihrl[NL]ucpr peair nlbmc msfg[NL]trv ppq bmo xqd vbui yegsr xqxawu fvuz aclhspo wnan[NL]loiq fvg kare rmgq hir rzo ossd ziw renh ygtkjys vda[NL]xmans kio alexs ujekfl vvf ddghn[NL]fcxvsf bjuytet zrzsobo uhn mlfzhlq bjefs[NL]zys htlqvky plno pbcqfuf fjwc vshkxrl lonp lyzmy dqmui vyyc glad[NL]tlc krhcter krhcter bolk tlc opryl[NL]idcii dverl uswb wusb zgax zhbt gjsnlso yhs[NL]cti npri rcbxjdw ollj nirp ghfvxzh[NL]blyhug aflnrrz zudyw ccnstq cyoju jxtqoj ntuknjq gunjiwy ycuoj igac cqctns[NL]bul yehpnw jifjrhc ifetu ufrodp hqzpeqf hdvpc qtvgxg ibb wcxsitx xztshb[NL]xzct scetn eoaufyo jtudgkx xrpgxip lpubtq juezstc nuc hokswh obkf ipbu[NL]nfq lwpmn qltal xnphsqs zlrgf iewtrtd mqzsob duokpy kfbqs icg[NL]vil zjz xkqrvni uay ystq[NL]terrrnt lnfg clm lbs ptpiy ybcuup ayzjm pqugx lmc yppit mbf[NL]dtajh vqivg vnblt fmn qxkw stiwna pclrrr fro khu wbslnqp tjyosu[NL]uqlehn tjuiy obt uedct bbwiq uxndqn[NL]hiqfovy xiimca zwne ivunvjk cmctzi mxnnrx dclib xzaoq ieztkg[NL]shpr xuorihj chuwq poadbo mhtvex gymsp iltgl sypjfua fmyh sgiv[NL]alv nxjt txnj bhact[NL]vjvtrex obmrxk fgigs meixbc fggsi awi rxdjpeg[NL]ypwo oicmbdw xbpeeyj uabzj cjvutvc oicmbdw immtmks[NL]exijri hogl epr gzdqyur xiiejr pre ihzlgzu[NL]rlh qfhx lrh qmvrx[NL]kogq okhd mivmivb mivmivb okhd[NL]taekt nhjaa znbaahn iaospxy jawwf[NL]ytdvq ghtqwud jkiig mre kzmmjxu jba nwpykc[NL]ktyzr aczd exgadhb uinrgac izazxky yyfe[NL]yrifb qgc lsiuapg teyelxn ugezu[NL]wdzkc ltx fkhncb hwrecp kfbchn sfcpc hjvq[NL]rjdjyt ahwxh nvggsmx lmz oshd xbcik powse ahhxw yhiq gxmgsnv[NL]qdr qjnam gag qjamn kooek mqnaj[NL]pza gml opf ilfbblu kjp luilbfb rhfrzgp ixagj ofp[NL]yphz runy dhull bozcsgk wfxekrd akgkbz urcphc[NL]tfyxwol lhcl npik beug[NL]szatel yfkve yfkve lzqhs[NL]yjzqon pcjibu bdncmcl kczuymm pbmg nyn[NL]rerqvs aoxucwi pmstl sstawu joqu abvcchg mvgjn mslpt vhmfkr utusuh[NL]gqbec jjpqdh yeaiavi nledfi jhzwc vyxjpf momnm vnknjs nvgjzik ipm[NL]psirt rispt lrkgma irtsp[NL]jbbaph xvunete gsvnr mjd ifxhpry cpsx hmuokkx vhcm yth shrrl zbhd[NL]gfa bcmlxtf sqyanrp cugg qxfvftz pbl ujsgc jajxltm gugc oil[NL]xjuhyg aht vmyvzhh oby oyb ybo xbybgmx[NL]atfk qjudfzz mky tfy[NL]nxk yzy jqgg qxgjt bevvvv efi xcbw bohc zaqlqjq[NL]hdc qpnx ygmtqw acvoa udboxw dhc klh mwgpk xfpuri[NL]cycgbkq skwhyf skwhyf veaqss skwhyf[NL]jnezf jowjt vsdu uck scgxd fvopomz vfajslp[NL]djvi epgkyqn apzd cpm owm kpwih fsr adlhqu jicp pmc[NL]erxlmhj wqxvofi ugj ttrmtsb[NL]omku vmrgoy tdicbje ewml dfnwbap[NL]gpih pyt ptsmzc gmdbu rqxkqmz objm nurxjz oozbere ztxug koth[NL]jpnl jpnl dmeh qed[NL]intdwv ksgw qwlzhq zpd lrl mwjl dozrjwq aujbet bsnf vhqyg[NL]eqs uot qyz xor aem kmrh mrhk jqx tsbrf[NL]irytjab mdzm qbb kkjt gofiwo xgbovg kyeyxqn tcks tljhx[NL]zgejy qodgah nqavvx xnigdvt[NL]eqve bizrxq lkhz yzwxgt nwe zfe sxypkz xnssept[NL]bxqn lkfg yfxbszo sphwifz wnj crhbq dvokzw[NL]vzn afatwye ogzvnu vnz rfjba xtugnj kpbgly ocsjd[NL]xrc cxr rahv yvhk khyv bed ctgbuq cmqwpqa jlbg hpj vmesvw[NL]jbshkya dgqw lfl mzcch jxsg czcmh ifruvlw ufwrlvi xcczlol cqqchmr[NL]rbk mhn tnmqdc sxnnn kvoa mhn sxnnn mgemob ieiyajs[NL]cqi ghxg ghxg ghxg[NL]uqwdxn qli gdtkngp gnptdgk udxqwn[NL]dmcczr dnjaqc qwdta rhrbi hkdwe qdjcan peic iulaz xns[NL]tcmppb nzq ecy sitdud nft ecy afrbf wvnc vmfpzx tcmppb cgb[NL]plitv efnpq mjqav nrxxo izg lpitv rwbzdo rdbzwo[NL]day dntga adtng agndt hhvtd[NL]yrg iudsh gyr ryg[NL]qttyeew tco flq bszw jkzftc wdh efcwnp mja rfmju[NL]moch prkze uslzyv plhjuy kxczyq qlmm hgq[NL]xtg ypz izy ixg bvs xlqgj xcy sepza abiylsg[NL]wxvsxn bqag jnlzgxq ikxwa dfd plqxl xlgqnjz nuqvoyb emhodso gaqb[NL]bzjdsm xmxkj fhuqn gauyw ntl kjxmx zcxdr vrds[NL]ofjcc uxyzlk ofjcc ofjcc[NL]zwosex kkvwobl cpudsmb kes zklf bayuojr otqnyr udbbs[NL]iqpvzh ybds piovrh oivprh voprih pov sfl[NL]upns cpeelht xboyk itb hsxdmt dnwgfbw upns fygf kwdpxzm mli dyy[NL]djwutl sikh shki ikhs gecd jqkon trqyw[NL]prbbdf vdp bvvfjcg ydqb muxygg[NL]vhpurzn psemqe xwqfk hrvonxu nxkxacq[NL]xicmhss tnpja qiad woipfy uvadcq usljh hzgs jntvfv wzikk[NL]mmupc twntp upcmm pumcm[NL]qnisuzy lppnfd uiqr eyqbain uxlp eyrfwjo olgkrps sbikam zin vckr[NL]nmokl skfni jcdfot njzqeaj nqzjjea[NL]slmaxx offfzqp wudicrf nfn rwfcdui cwirufd[NL]paffi murnjd oyj lbtjdqe babuas dtqh qkt stapzl yrqlp[NL]eedc rig zmnfmn edec ecde[NL]bcfdf edovdj lacx nzvze sordvxj ybs ujh zvvvp rzstejg ueosuq[NL]xrrfsd okuvem znzlvmb jwzcb bfg bmuxbc qzwfry[NL]pqgxybd cvgra acgn ocd ancg fvfcx fbb bfb zfzv[NL]tmmv mpywyg fwl bnvcv lcnv flw[NL]xxnfbro papc ianru beuzx apcp rnt[NL]wuyhycj nrnc cka ebg rncn rvo wcyhjuy[NL]thh cmoog hwf imqfp okzpxd[NL]rzxiqt rtaiy ytria tyria[NL]cjkmro myif myif xyirn aqxlol wlhwibi dhzsen pzwgm bfbz bufjs qwffg[NL]mxhiui umiihx zomyll vfieccs[NL]yyntf rjk iivgj mwh rjk[NL]dsshx wsmaxhc xcwuelh rdsgtr wsmaxhc rgtsfj[NL]rdh nwlxiwu xsjzbpr bsgps[NL]ufyo vqtzkg kpeohu mxzt fyuo gawgaq youf[NL]hzbhut bxsnjwb zuhhbt zhhtbu[NL]pdz sgntypg ragev hrrji goitft yphnebs xjzoo sqf jsuzijq dsocb hcxg[NL]pptsq woomypc woomypc woomypc[NL]axcg wfbnpql ejqb cmnn nncm csvlc wraludb pkmp whtht tfpicer[NL]moom oomm ommo vfqeii[NL]xvrgpp rofl yxyrkb oage nypzau pwfnkn jxnhkw cyxsi clzb adwpuh[NL]mfbz vdtt muzhm wvwwfl ttdv[NL]cpqgvbu byc pgfrlkr aftl tqm zcqxi juu gnf ppovxh huoa[NL]konpcp lzordid jqng lwxs nqgj gghkxmf kyn ngqj[NL]iorhccj xfygc cnfr tysqc xpcyf vmjpitf nut zmrk mgbrtb tcblxwf dkadwrm[NL]kov jtmp xoatesx qxkilp rmggpfx ltpxzwf vko reqms mqq nps[NL]hjigmk fyqy wpuwe mwmso thsimfs okcmeyh mzqkez duzaq vzhyrm uyvpkox cwivpls[NL]ukoerf korufe zhs ntwfz hugem vriyk enfaib hrrcdgf zllsk vkiyr[NL]shkx khxs wntpjv qdevaw noqyht nwpvjt egh hgok mukdjfi law bzbvjz[NL]dquk kczxsq tdu trnkjs wqtdc ybvcb[NL]hlrotxn cumcjkm qwufgle ylm nejh hnje pvaigrx myl sfvsd[NL]szmvisn aywic vsnimsz iufmybr[NL]zjozr zojzr qmn ffrggdh wam dafvok[NL]nxkvlhr posmf posmf posmf zhlzb[NL]ywis kpqpyb qae zqxpuz pcj hbsfz ejlwa lajew znuom[NL]qxsl ussivur dstd avojo[NL]yoeagao egpaqm ymzf kkauy ivm illir wsvchne skmamvn nqxc[NL]cldo ixzzy vhk nra zhypgab[NL]qjdd ecxud tbuqq mpotbdk tjdpczn knncm tyy[NL]rbfc fhhjf innia tsjbbbv fmtcuup rapvhqz ebpzt whdbms gvjoy lykl fquvcby[NL]bihhfwi lhal udxz uwjwp dmb[NL]fekxamy uophet yzvv rqj zawlp ldrv mdymkzy taauf[NL]rcwxvmh edueui ltdyo xfghz dgjig senm ifj[NL]qcu fii axmgijj ifi oixjfsg jxagijm[NL]sdtyr rbdh yvnvq czzuig wro[NL]lot xkto cmpiena nht ozcg aotcw xiegl cyaouj und lsclep cexn[NL]pgihljk cmgmv sajhi zfvbqij ogwoc ajsih zmppe[NL]jexwkdp dwpexjk mzjydfu bff rubgdb[NL]yshfhx emkl hshxyf mkle[NL]dxgti jdo tkwprv pbxbrqd oiz gsbdphd qotu utfdnq tzvve bqc[NL]ovdf bshfxyl xspjpd vljdsm mgkd djlsvm mlsjdv[NL]etyia eytai sfq qafj xzgp ewhsn snwhe lhqp[NL]zjz mwh dorxm ges gexo rckwsa dltoq mmntha[NL]hqkuj ypsjcxo dixbe rmvnhjh ovnr[NL]edc iffaxc lolu xwrvpb gva vti vit[NL]ceuxq xbwejr lzyvm rozseit cwe mham fivpwj qtv omaktaw[NL]alzdrk tsxbuld mdbq pgbdtoo xwf vzalric nqe jqwlxsy cbtylu dtubxsl lqm[NL]rqjmjcs exjpn kpilcgu ihcm lfadjm mlri hpd vqs cxqwqhu twxrtk[NL]aeuvlcp aubvnw riedvz arypagp uuvg kliehx cokt ogh xsdw cdsyywv[NL]ddwrgvp bscaq bbfv qrbutp[NL]jpdg uey eyu uyarl zgbk qyhqq fdvlql zmwkp[NL]kbt bkt lebhpfu smrzt xalw mmwa zmtzfry tkb[NL]fcvcv oewfzu fvvcc mldww lwdmw[NL]ejrltsu sqoyx wfvsdbp bfdspvw bfir jqhgrmt ofdmrjg ysq[NL]jzwucwn erqjd eikq knpf cvk xvqnscy eei wvfjzmj xujq cqaim boev[NL]jqhkmr ipjpj zwno ybu krhqjm zqfyyzb dyciy[NL]ugwsw rpwteje qtvwi pwyhrzt hfcdfmc qbovk ibws[NL]ffy kdder qjookz bfvmvvq yjzuaj fvxllfb pjyz jcezhye fimyydt qjookz qjookz[NL]loupd nwsc yytvuqo ltcqxnf[NL]iho ulvxguz fxbf iqu ofjtmvq xhs ybbusd kxg mebdnah ucttcf zufb[NL]wzdb wumuhtv kef aavv buu xmjtlur faaccl wospwff bjasr eapfsi[NL]jau qzszci ciu inagax[NL]kui tqig fyovsp fvwol fyovsp mzth tcp nhoq[NL]ajdla wtpj amylu jly tvq wjqef[NL]ofqc einz bdze tows bdze eew[NL]avwavzt aesrsjv lbmpi hllv chdbul ezelxn[NL]imcprs cafb clfg rsjo iylqu nvk vkrq izezlnu vkqr tyhnv[NL]rwj zboui reh buzio wuhpvid cpzy jrw tsbuiby hmxwqr ute[NL]ixq luwbi uoiwsjh souz ysoubw uilbw ffwjvw ewzswoh hci zmfdaov whowzse[NL]xrhgqf xrhgqf giyv giyv[NL]toiqgzv gakg udgdlb wvi carrn pjyha muqclu[NL]wuxthi srtszr ourab hpds bakvy fnk yefe yfee doowxcx[NL]ijdc ujhvls xmy hwg yetsda qelbe nang xgywo wgh[NL]bhm icq cnam dec enksf qfctz pwxoo bdf cnma xoowp rbls[NL]jguzh fextz yax kesaunn waljo jltcza tfzxe dezs syi ebwxnks[NL]flvq bzgd clvqw ahtyvu xbdyv wssxx boscm grgl nqcg[NL]caskpli hqctxxc nwpyo wjlqfqf ebti dva[NL]wmsz fzpd ikgeq gti ejftoou ezs cqef mybojc rgwz[NL]mdaay yfppa pavl fuuvfkh hpod tpb dhxmia emdecm rbqcwbk vecyt[NL]neha rmvl ndp vlrm dpn debghi vyhvc[NL]bnp zkxdu iqqkesd abtlx hmjasdq kyvekr krt srrjyd oxmfev oot[NL]dumlcqd ccm hyir oritdz madjjw[NL]oakqrs advfmu verrc zkfdcn btndsp[NL]onlkinl rdtm bscfxre bnu oumyrvv kgc zkj[NL]tfxfsgm uwmic agswclg uofezgc[NL]wpfdyjn kjlihk etbot fbu scm gwccce xgownte wig cuaijbo[NL]bzbdk etozk qracb oftfoo lkooe[NL]xupzw vmxwu sis wzpxu[NL]gbz oqbgh jwgrru bzg kwmxcfc jrurgw[NL]agyjnyc tuec imxlult omwiyjg fiwnoqx nhmnro qtg kbr agyjnyc[NL]koiq llreotu elrtoul dubfvgy whq[NL]htm lll crzppb gdjaae nsmxzh gnfvn obiuy ymspzbo iuboy[NL]thm xlfrr pbxdfo mht tygi sapxgbv mmngzf dej[NL]eus seu qmstw ues[NL]yvfsw esut biblze kbjcpk estu xih qzki ezlbbi blzv[NL]ohq ugc tqqeo jygvpwm vfs ldnfibp ycbpa sml rmime[NL]kuuow gbg nzwdaf wiimtg lam oqmm[NL]wsbwkdd hda nqk ticz mvt[NL]gqbljyh zqugqs cjod sxwlqy qkfs wwvwvt dsojb qbhjlgy riusoa uosari[NL]jkphfx dbt les jsvoij rnuw mxmmchu dol vto swn[NL]qqxe vwvephr twdqlyg cvdu xjiych clooq vkwavl whvverp yuz vkwval[NL]txtbudi tiutdbx wqhx tws utgbf amh hmf izsez ooz[NL]egdube nhsxjs nxjshs xoy sjsxnh[NL]egdziod diodegz ejxn zogedid uhhkr rnm cyvvuc uqbl[NL]rbn pinwag sidwdwv jqdbe jlbemk blkeaqq ipfqbtn zkrbp[NL]bdryz sbh wxvn mhot wemsfm oemkff[NL]vxyn xvdwwo xhd vyca zxjaw vfkz xhg ofsphks dyq mmzzd[NL]yjrqsjf iiesdh envwyx rmtbmiv ggzsg ukx bprfym qmyqc vag ymho hjtoh[NL]fuxxrd wbweptd vkoffr wbweptd[NL]gfwcez smetli yjyh pslpz qyokpsm qsy cxjymg wqfkf obuq awz[NL]eqhm ceest kayf heqm[NL]rdi dti vntcf ewkmpvf jjwoihc[NL]sfq qlb xrm ocy vtnj zdznbal zvon stln zwnj wsgalvq vhphap[NL]pya jay mgnyo pya xmapdn[NL]hrwbj xhr gvwl ktq ktq gvwl[NL]rzgqi hjwtthl kxhggbl wepc hgavj ctmqug[NL]tzfwkc xeqfath iiuwq iiuwq dhwuvy[NL]gibagy smq getjofc lum msq ulm xuxu bilrus ily[NL]xlv ndrkch hdcknr nqltoze xvl[NL]wmc vuzlrj mwc atp cvpx atv ujatz[NL]hxpafgl ymjltv nvvpy ahycdk jhpdcks ettm lvqyw ertpivm dnezwxx usi kdhcay[NL]vrh hqyomv mcq ilwjbkz yprjxad[NL]ugv szfitxg zeluib pfj ijm zmiigxx gltxzz jzljhgh otskue[NL]mxp bilj jlbi tce yfted zxsqas ftyed[NL]ykasqv ehye kirmnl upmi dojwmw wzj ykasqv ifixn vreoypz[NL]kerbgub nnroqk onkqnr gbebkur tjhl knjo ccsem yozvrcg[NL]ygq evkoj wkn ffljhds scxeibh egsybeg mwvi vgjblj qda ywqpp[NL]hocvpl ozgkxp xgmj ejzyxm[NL]gernu kks lxe nxzv sypg xle goz[NL]xoatis fjp wzlbo dzkonz jtutyj vdonj swro tqclemv xhomap ymeqkua vaxcw[NL]mxcyjs ywyxndk wng vpftv nsuvu[NL]jmiyyhh gwser shgcu jmyg cjzegc hmhe eopg kmkan[NL]smdd dmds mgqhtkh qtamih haqmit skkcy[NL]dnj rmggy rgymg uburbao rymgg[NL]klcpjgq ons ajyv sqryt son pjlcgkq xlobdt[NL]piw shonk tzi mcdumz noskh tebolw yaypn[NL]ozm mvmjgtg nxj weommiq asnmhzq xjn uobztuo cqgjh utfb oydt ommiewq[NL]qlwgsc vvpe xgft ahpjc zjtx iyof scwqlg dxgcokx ltrefj xyzq rwto[NL]ggqdd dqgdg ggdqd kjkmmfp[NL]htzjam fjbg iagc xls iagc iydtf ihxl boa iydtf[NL]vhe nqj bwgdoi hhaoa qtulz[NL]axvyja hpdkwee hnryj prou rgadv oubjdqg knjbc[NL]caz xibj wqkzwe peioeya vmz hesy ftb[NL]dudwcr gupj sjrtzc xsqbb hiet nujv bebcvsj eks uuzlcx gex[NL]kywozi tfzuc mflssw hnxxxqt zzc tzfuc hkokuv mnjg lwkavjp lvpwjak xez[NL]izgh zfv cingjt dkf cknite qox vfz zvf[NL]ojpu dzk tehpgnt gntpteh[NL]glxfxa uxq ajtles ahgzn ajlste zwgc mrpu adz wuunwhc zda[NL]hdgdtn hnoyz aromkb qujfv yjgmn tbf atw[NL]uyvsv oaopjv uyvemxk ldpp tthe iisjk txr hebmd yxevukm rkziao znt[NL]ypdr mnwuzvw acpg kzwz ywbn wcrr umrnlbe lkult ljify azyhu mgqoo[NL]abmpl omsd xmyl mxyl mgoq kracrf ufm ppwi zpggh[NL]uxfdpv jnm vvc vchunhl ubv ktj mxolsxz[NL]fcja eci edzrb nlvksaw lhf ycohh tfztt xso ceub tyv[NL]rkwtp tcmmvv kufg cxui hdamg suuaej fgku cvjlv[NL]oldbgy riadoyo djsi wca zxoeq pmemqap aijxa[NL]nyy ruxcosx xisqoz yny jvzfpbe tlfdiaj ybd jifatdl zuzv[NL]kxwdz qvrvx svllp ergmme[NL]swjfuv eronk favcxfm acptbh pnbjn ciqcrlt rgvdnlt icgahb[NL]ddza xxfn use obqka bfzwjp gmf bld fyvde mxdfdl[NL]ame bmxbyf ame bmxbyf[NL]rdgby pyfog dybrg gdryb lpztd[NL]sntg impd uxgxai naoalb ntnk xgix[NL]oadpmqj oso criln izih oos[NL]ouzjq gtl ito xefqt phnv ouzjq hoyjjj[NL]mlp rboq lpm roqb whvp[NL]tghcw ggshevw dzsgj ggshevw kec ggshevw[NL]kmwhb kfcb mbhkw gemz fdh[NL]euve veue kplrq evue[NL]hikfiw bcdktj hcnawja gjasvwc vcht igrzly rkxijxe ikfwhi dvmp[NL]hvksis kafs ktcs sfyqzyt etctrgt vodwr wff tskc juobnm[NL]dpcsodn ehwc pglywfl yhdp mdiyzx[NL]ibog umftejh cfm pnxhna wqwx yabnk ygws dqw[NL]dezz tqw qism rarfe fpmlab xvbau irwtfs wwmoyss yvn xetqp xtqep[NL]pchqwk npsmd jefec qok uuc ucnpz rlkakn[NL]kudh rjysb xrdbx bkbmjfo xrdbx[NL]rogu ssdwsus voa ncw obkxsr[NL]tflf hlevus scq rrbpat tau wxsq wxoblt[NL]rzr lex kqdy whtj ffnys xlgkkff msjhy dimaq hrc wyde qkwf[NL]ghtwd wernjpn tdgwh olrfvmr edq gxvp[NL]rjirvf skhdgln aauit bipu mubjiwp kowz gyjfbjx cmgdqs[NL]aftfpbv agajyy aqjll vsf twh robpys lebt eav yribup[NL]sby ymkla sxkbfwl awmd nhb vlp[NL]kizvjj ycjswr jkzjiv vuy jijzkv jcs[NL]cwvch xzqfal tephz lqfzax cnkbdcr mql zflaxq[NL]jjxzwl himpra ssjf bibfiui seeaq pzse[NL]jogrn jogrn sqew jogrn oixgwr[NL]khonpyw iiyxir vybhc ndnxxv kzlt ipmncn[NL]okqkqu svbemi nfn ovd xgwy edd ujet nrrbv dde vdo[NL]jobvf dus asvio vaosi sovia[NL]knmz qbz nkmz zmkn[NL]isbmopr unduey impobrs hea zswciev sopbmri duuj[NL]ocs ntgnrdu kbvtzp cvyieu fiyn znmh lhrz ixtnzrj vktbpz lbpqx vzkpbt[NL]muduhc sabc dlyoisz kuaz ogpyepw yuog ictiiqt[NL]xjflsf nfklvml thfh uajnmby cichyj xxoqi lpime bxpyx[NL]riahifn bohbgd obhdgb jni qzvkf ybp hjkkwq ytutd cakcsh smfdoe tuytd[NL]iddku nccp zgtl yne ppzpqcx lwm[NL]refpcz uqt uqt uqt[NL]mtn czxkagb nmt caqacrg bcakxgz[NL]itxjii uethxbj vpds bsqod diqax inv zrwt doepe[NL]bfyaj nbvhg zmi buf[NL]dtre dkwdr nrapm qtfth odvt bbcnae vxuk gqm enlg[NL]ybt qcfozrk yzrh bfp euuozuz pzsdkxx mhi nbkzprb[NL]vpuhqn gyx caint antci vfep incat kqdakdx[NL]ddhi chgnjk ibg xbemitr mjtdph eovw[NL]ngbtuvq qdttlsg dbqhhwk bkrqze qdttlsg qdttlsg[NL]evn smvhi dgcmn xjo ascc ahbpj uvzc pwn tung[NL]ksu thr omg onvsqzz rllakar ysfjtfj grxwyx oawix gpk suk[NL]qvb iouav yhtndkd vuoia ouaiv[NL]kud kofcip hcczrgc cvvxxlk rvyamwe duthdzr dftun[NL]rgv ynw gph tmxwfup nwy[NL]dnc trawj kwzbx trawj zvp[NL]ogqxijy tbqtsg tbo vqinnlq jbvgl sfafh rve mcxqs ubh[NL]qccr lpv puuvdyb tydaflf uxic[NL]tlon tbfwkxg tlon tlon[NL]iytiz qjlqaqw uixb lnt zwro uzgxqfi gklgnqs zwgoidw iifk wkwdo[NL]tmvhxw tmvhxw tmvhxw fhiqpjy ejk kvysd[NL]cmphg xjjz groiccd dvetuk xbwa zhm lyi ohhd neg bxaw yil[NL]kdmzopy lxx bvhach goxmxu qbqvzcm qbbrhvb nrfom aixmio grpxz hbrqbbv lkucih[NL]bnqn phqr uycuxc mopyyfh bbpesqm stgigq stggqi cwtjm asqhpl imvlxj lbmloo[NL]pws iuvbvjr cwccm qbr srqnstz cjebq[NL]bfh jobkcy gtbroe lpagq icmax jobyck fbh[NL]ounqdo qrrr pwi alho rrqr beao rsioepe[NL]vrccqge qvcgrce cbslkjs qnclw rvmjkw[NL]aaxjns deupjs wtgxtp penad depbho tbrdt depbho qxg zhjxpgd[NL]drqfo kbp jfa jaf[NL]izn oczcitj cpae quvzqo iwwk jck idjdpm[NL]ecort zgcvxx bvh vrprsf[NL]fhubfvy ndcfjo kol hyufbfv hvpka[NL]kpt zgajpc rjvsxa gayznjd[NL]xeoixk peq kfu lqa mjnv mzvh bicl hlfk[NL]wyt imdx lksy twy[NL]xeptp ilxs qbsqzwn rsy slxi xtpep dsdkekl[NL]rotvbt fuirp elos ciu nhx bxej trmtx ixn xbpc vrxtma[NL]skcprn yns sao ghlq vftezvc aaryahy telt[NL]fkaov gexa xijv yiksa xega dhgw okfva gxxs edkecag mqbqvrm nrzcqub[NL]ljc jujxeof fdj gdzjzr mabbktu pmyrfv uspven zxry snt hrah[NL]nhujhdr jdhrnuh midm bbavhpp cpjk zmpbasz eptrpou znq zqn[NL]ywzfq wuu lfflon uuw rke qzwyf hjbms gakx[NL]yqrq zsk jzn uuuzrml kzs lseupsg waynfh blech[NL]gwyqej weyjqg uwuje uujwe[NL]lxud rnwkc bgygkh csq rfvtos ystqp keb gkakodj uthcce eqxifl[NL]elvj evj rfwo vvgkosh aarcgjs utsbh orwf jxcqvmh uowmktl qtgf[NL]bqszre oxntty ombwiz mbiwzo[NL]ccp iilcc tacf czk giwv erqi jgdfah wip xtrzhv wosvbyb[NL]gymyw rwsxeg gvydr izyk spsonkg knospsg[NL]djj tbr tbr tbr ice[NL]yyzh zkykapw puydtik ysxc hjumhsd cuhhw dnnhida yyzh lnklymg[NL]nhbcxsu ccrbbyw scbxunh ghxrkqh brcwcyb[NL]latdaav sexa ipzuzjl ayusb etb fshh[NL]giz akqd vjmabii arfuzgv efrww jxkvolg efrww vrnzgbx[NL]jmcc vqy adkzj fqrkdo tjrczp ccmj cfponk rptzjc[NL]jsviu sraw imsj fujm cdf xwqhl lhz ojejzuy trtqblg[NL]ibz dulm muoq quom etvjzxn tuhrpp jfukac jqctqn qhgbae msgmcit ludm[NL]zgx bpfa elhp rnyqtq wyceube nkeuxz[NL]lzxfo vygpecv jszacku zfxlo[NL]cpmv ysaaj xnp wbvqg hrsiuj venjxna yeqvwmk ftaga dcqxc jgapb rqdixp[NL]xpbbe tyn hfdlu fto wrgzkou sxylv cqto wdv xqc pnu rapk[NL]pkrxypl wnu oipq tzbhnc gpug tgzf ofjb[NL]mvaz bwcv gll itgcye dessw szt gzimgeu bvmohh wbywyhc kzerxbr anjsive[NL]lhvnrzs qkmjwy pnyciwp mgp jfdz ghvtf yusfzg upab[NL]xbscukx aubulj snbcmc uscxkbx ddpucyg[NL]hgv ollh yzpjmpy fcicyae vhg gvh[NL]prd onyd iux oik xui[NL]zipadig nvewx cir lbpcusx dljqy[NL]ifyxzsc btmy lsu tmyb lus ldyzx[NL]egmyxbe ieasvek dylmj qahtatr uyqgbk[NL]mejjczw spj vaekp kdud[NL]vwan mgenld mnlged vpfuil euoxlr rclkpi dfknyoa rhthij kcyxl qaxab crlpik[NL]pqm eihogk iwml nuauxi ngilkoh jmu mbdi cqxz nblb rmuj zczdgp[NL]pswbe mtzch wbeps fxtnc psa aioff pas[NL]prwrpvz oadpqvz tgzrt giom pjyihh rxdir dmya xjolzxv[NL]khdybe obqkjn kdq jkvmgwo enpat wyw qjbnko waid msest wwkoyts[NL]yep liv ofmtpod imdd qyw[NL]afnrx jgn gxarpb myltj ggrsajy mdaobjo vbtn vbtn zlziz eds[NL]hqr kqu oub skoeqk icnfm cqvld aay bto[NL]rga odaf exoosh pwevx zpbd plaa xoseoh[NL]mbr gqu oxvchrt nqa larxmjx pfozej[NL]ozuo ywubjbg xcua eblwqp nfdvw hmhen zkjfu gmhgp bsyi ktprtf[NL]src vrysby srybvy znwjm hmypwdl gdmau pqe[NL]cldr crhi lbaq fbuduyn hygbz uhida[NL]qrxukq dygkp oaks soka oask[NL]vpido ajgfq pwlv hezt fmg epwrxo rqvjke iovpd hhkjm[NL]anxf ydl xnfa hqph olorp[NL]exydcg onxjm psqlbv ehz boar hze qsblpv[NL]mnzrvc ipj swg ijp sgw gdkntsd fzz grqwly[NL]erpq qghpj fay gci uglm afy[NL]jwbq hbxaub jpdilyt yvalrlk topl qup[NL]eczonk ftcc paltirb owz tihhe dglxory wthvqcb qdnxm lirejh alyxsr[NL]ooruaby gboyeu lkv arrz jcqyzl uxlfk fhmeony fcmh[NL]wzr xjb pwmf okqj adwcedy lkidve uwekxf asbdzr biub[NL]dikhur pxgh urdinjh wednf ulzdxs[NL]iplf byt tyt qnnlba pzt bednml ljjtkvo tjovlkj uwms xat[NL]htzk ltmfha xikeze atfmhl fchxhyz[NL]lqala bqwgcul vetaa xuxjau zcb wtdmomu wfqmpq sief uyblyz ahv[NL]aytvvo awm ojaaigg awm dbfaokz[NL]abq npcyld fzbfku oia qss jkxldm wgtmki pasgxi dieix rpqnuac tecnfy[NL]nmr qzfj qjfz lsz vnahex[NL]djxoo jzlkh svy xige[NL]tjlkkg glcuvmh fwzlhi ecun qlgulj hrfhyql qgdlf ofakqdf zokkvm gelxkq oowgs[NL]upfpk gfstjlv lxc rjd nhj sbq jpzsz zsjzp[NL]favd nzqfdid nekfjsf mtjndu[NL]sgdqx uvpuefv vhwrgd aivav gsqxd jdhfoq[NL]llaf cthbgy njrpw fqgkx jzf xqkgf lnrfrm gkxqf[NL]wzdwlc wisst alw kyjeur sjsqfcr tta bijnyn whfyoxl[NL]dtjr baxkj lmnyrlg nrmyllg[NL]mtgky xmwf zdko nnocxye gytkm ygp hixk xwmf[NL]maudjy okgjga uadjmy dzfrk omd[NL]azz ajdcqkd bcafn zaz dcjaqdk gylyzo[NL]xzvfbf fopmfxu mvftgr mfupoxf coyhof talcc vpkslo".Split(new[] { "[NL]" }, StringSplitOptions.None));

            Console.WriteLine($"Solution is... (drum roll): {solution}");
        }
    }
}