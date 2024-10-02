<Query Kind="Program" />

void Main()
{	var defaultFormat = "DD/MM/YYYY";
	foreach (var language in Languages)
	{
		Formats.TryGetValue(language.Value, out var format);
		$"({language.Key}, '{format?.ToUpper() ?? defaultFormat}')".Dump();	
	}
}


public Dictionary<int, string> Languages = new Dictionary<int, string> 
{
	{1, "ar"},//Saudi Arabia - Arabic
	{2, "bg"},//Bulgaria - Bulgarian
	{3, "ca"},//Spain - Catalan
	{4, "zh"},//Taiwan - Chinese (Traditional) Legacy
	{5, "cs"},//Czech Republic - Czech
	{6, "da"},//Denmark - Danish
	{7, "de"},//Germany - German
	{8, "el"},//Greece - Greek
	{9, "en"},//US - English
	{10, "fi"},//Finland - Finnish
	{11, "fr"},//France - French
	{12, "he"},//Israel - Hebrew
	{13, "hu"},//Hungary - Hungarian
	{14, "is"},//Iceland - Icelandic
	{15, "it"},//Italy - Italian
	{16, "ja"},//Japan - Japanese
	{17, "ko"},//Korea - Korean
	{18, "nl"},//Netherlands - Dutch
	{19, "nb"},//Norway - Norwegian (Bokmål)
	{20, "pl"},//Poland - Polish
	{21, "pt"},//Brazil - Portuguese
	{22, "rm"},//Switzerland - Romansh
	{23, "ro"},//Romania - Romanian
	{24, "ru"},//Russia - Russian
	{25, "hr"},//Croatia - Croatian
	{26, "sk"},//Slovakia - Slovak
	{27, "sq"},//Albania - Albanian
	{28, "sv"},//Sweden - Swedish
	{29, "th"},//Thailand - Thai
	{30, "tr"},//Turkey - Turkish
	{31, "ur"},//Islamic Republic of Pakistan - Urdu
	{32, "id"},//Indonesia - Indonesian
	{33, "uk"},//Ukraine - Ukrainian
	{34, "be"},//Belarus - Belarusian
	{35, "sl"},//Slovenia - Slovenian
	{36, "et"},//Estonia - Estonian
	{37, "lv"},//Latvia - Latvian
	{38, "lt"},//Lithuania - Lithuanian
	{39, "tg"},//Tajikistan - Tajik (Cyrillic)
	{40, "fa"},//Iran - Persian
	{41, "vi"},//Vietnam - Vietnamese
	{42, "hy"},//Armenia - Armenian
	{43, "az"},//Azerbaijan - Azeri (Latin)
	{44, "eu"},//Spain - Basque
	{45, "hsb"},//Germany - Upper Sorbian
	{46, "mk"},//Macedonia (FYROM) - Macedonian (FYROM)
	{47, "tn"},//South Africa - Setswana
	{48, "xh"},//South Africa - isiXhosa
	{49, "zu"},//South Africa - isiZulu
	{50, "af"},//South Africa - Afrikaans
	{51, "ka"},//Georgia - Georgian
	{52, "fo"},//Faroe Islands - Faroese
	{53, "hi"},//India - Hindi
	{54, "mt"},//Malta - Maltese
	{55, "se"},//Norway - Sami (Northern)
	{56, "ms"},//Malaysia - Malay
	{57, "kk"},//Kazakhstan - Kazakh
	{58, "ky"},//Kyrgyzstan - Kyrgyz
	{59, "sw"},//Kenya - Kiswahili
	{60, "tk"},//Turkmenistan - Turkmen
	{61, "uz"},//Uzbekistan - Uzbek (Latin)
	{62, "tt"},//Russia - Tatar
	{63, "bn"},//India - Bengali
	{64, "pa"},//India - Punjabi
	{65, "gu"},//India - Gujarati
	{66, "or"},//India - Oriya
	{67, "ta"},//India - Tamil
	{68, "te"},//India - Telugu
	{69, "kn"},//India - Kannada
	{70, "ml"},//India - Malayalam
	{71, "as"},//India - Assamese
	{72, "mr"},//India - Marathi
	{73, "sa"},//India - Sanskrit
	{74, "mn"},//Mongolia - Mongolian (Cyrillic)
	{75, "bo"},//People's Republic of China - Tibetan
	{76, "cy"},//United Kingdom - Welsh
	{77, "km"},//Cambodia - Khmer
	{78, "lo"},//Lao P.D.R. - Lao
	{79, "gl"},//Spain - Galician
	{80, "kok"},//India - Konkani
	{81, "syr"},//Syria - Syriac
	{82, "si"},//Sri Lanka - Sinhala
	{83, "iu"},//Canada - Inuktitut (Syllabics)
	{84, "am"},//Ethiopia - Amharic
	{85, "ne"},//Nepal - Nepali
	{86, "fy"},//Netherlands - Frisian
	{87, "ps"},//Afghanistan - Pashto
	{88, "fil"},//Philippines - Filipino
	{89, "dv"},//Maldives - Divehi
	{90, "ha"},//Nigeria - Hausa (Latin)
	{91, "yo"},//Nigeria - Yoruba
	{92, "quz"},//Bolivia - Quechua
	{93, "nso"},//South Africa - Sesotho sa Leboa
	{94, "ba"},//Russia - Bashkir
	{95, "lb"},//Luxembourg - Luxembourgish
	{96, "kl"},//Greenland - Greenlandic
	{97, "ig"},//Nigeria - Igbo
	{98, "ii"},//People's Republic of China - Yi
	{99, "arn"},//Chile - Mapudungun
	{100, "moh"},//Canada - Mohawk
	{101, "br"},//France - Breton
	{102, "ug"},//People's Republic of China - Uyghur
	{103, "mi"},//New Zealand - Maori
	{104, "oc"},//France - Occitan
	{105, "co"},//France - Corsican
	{106, "gsw"},//France - Alsatian
	{107, "sah"},//Russia - Yakut
	{108, "qut"},//Guatemala - K'iche
	{109, "rw"},//Rwanda - Kinyarwanda
	{110, "wo"},//Senegal - Wolof
	{111, "prs"},//Afghanistan - Dari
	{112, "gd"},//United Kingdom - Scottish Gaelic
	{113, "ar"},//Iraq - Arabic
	{114, "zh"},//People's Republic of China - Chinese (Simplified) Legacy
	{115, "de"},//Switzerland - German
	{116, "en"},//United Kingdom - English
	{117, "es"},//Mexico - Spanish
	{118, "fr"},//Belgium - French
	{119, "it"},//Switzerland - Italian
	{120, "nl"},//Belgium - Dutch
	{121, "nn"},//Norway - Norwegian (Nynorsk)
	{122, "pt"},//Portugal - Portuguese
	{123, "sr"},//Serbia and Montenegro (Former) - Serbian (Latin)
	{124, "sv"},//Finland - Swedish
	{125, "az"},//Azerbaijan - Azeri (Cyrillic)
	{126, "dsb"},//Germany - Lower Sorbian
	{127, "se"},//Sweden - Sami (Northern)
	{128, "ga"},//Ireland - Irish
	{129, "ms"},//Brunei Darussalam - Malay
	{130, "uz"},//Uzbekistan - Uzbek (Cyrillic)
	{131, "bn"},//Bangladesh - Bengali
	{132, "mn"},//People's Republic of China - Mongolian (Traditional Mongolian)
	{133, "iu"},//Canada - Inuktitut (Latin)
	{134, "tzm"},//Algeria - Tamazight (Latin)
	{135, "quz"},//Ecuador - Quechua
	{136, "ar"},//Egypt - Arabic
	{137, "zh"},//Hong Kong S.A.R. - Chinese (Traditional) Legacy
	{138, "de"},//Austria - German
	{139, "en"},//Australia - English
	{140, "es"},//Spain - Spanish
	{141, "fr"},//Canada - French
	{142, "sr"},//Serbia and Montenegro (Former) - Serbian (Cyrillic)
	{143, "se"},//Finland - Sami (Northern)
	{144, "quz"},//Peru - Quechua
	{145, "ar"},//Libya - Arabic
	{146, "zh"},//Singapore - Chinese (Simplified) Legacy
	{147, "de"},//Luxembourg - German
	{148, "en"},//Canada - English
	{149, "es"},//Guatemala - Spanish
	{150, "fr"},//Switzerland - French
	{151, "hr"},//Bosnia and Herzegovina - Croatian
	{152, "smj"},//Norway - Sami (Lule)
	{153, "ar"},//Algeria - Arabic
	{154, "zh"},//Macao S.A.R. - Chinese (Traditional) Legacy
	{155, "de"},//Liechtenstein - German
	{156, "en"},//New Zealand - English
	{157, "es"},//Costa Rica - Spanish
	{158, "fr"},//Luxembourg - French
	{159, "bs"},//Bosnia and Herzegovina - Bosnian (Latin)
	{160, "smj"},//Sweden - Sami (Lule)
	{161, "ar"},//Morocco - Arabic
	{162, "en"},//Ireland - English
	{163, "es"},//Panama - Spanish
	{164, "fr"},//Principality of Monaco - French
	{165, "sr"},//Bosnia and Herzegovina - Serbian (Latin)
	{166, "sma"},//Norway - Sami (Southern)
	{167, "ar"},//Tunisia - Arabic
	{168, "en"},//South Africa - English
	{169, "es"},//Dominican Republic - Spanish
	{170, "sr"},//Bosnia and Herzegovina - Serbian (Cyrillic)
	{171, "sma"},//Sweden - Sami (Southern)
	{172, "ar"},//Oman - Arabic
	{173, "en"},//Jamaica - English
	{174, "es"},//Bolivarian Republic of Venezuela - Spanish
	{175, "bs"},//Bosnia and Herzegovina - Bosnian (Cyrillic)
	{176, "sms"},//Finland - Sami (Skolt)
	{177, "ar"},//Yemen - Arabic
	{178, "en"},//Caribbean - English
	{179, "es"},//Colombia - Spanish
	{180, "sr"},//Serbia - Serbian (Latin)
	{181, "smn"},//Finland - Sami (Inari)
	{182, "ar"},//Syria - Arabic
	{183, "en"},//Belize - English
	{184, "es"},//Peru - Spanish
	{185, "sr"},//Serbia - Serbian (Cyrillic)
	{186, "ar"},//Jordan - Arabic
	{187, "en"},//Trinidad and Tobago - English
	{188, "es"},//Argentina - Spanish
	{189, "sr"},//Montenegro - Serbian (Latin)
	{190, "ar"},//Lebanon - Arabic
	{191, "en"},//Zimbabwe - English
	{192, "es"},//Ecuador - Spanish
	{193, "sr"},//Montenegro - Serbian (Cyrillic)
	{194, "ar"},//Kuwait - Arabic
	{195, "en"},//Republic of the Philippines - English
	{196, "es"},//Chile - Spanish
	{197, "ar"},//U.A.E. - Arabic
	{198, "es"},//Uruguay - Spanish
	{199, "ar"},//Bahrain - Arabic
	{200, "es"},//Paraguay - Spanish
	{201, "ar"},//Qatar - Arabic
	{202, "en"},//India - English
	{203, "es"},//Bolivia - Spanish
	{204, "en"},//Malaysia - English
	{205, "es"},//El Salvador - Spanish
	{206, "en"},//Singapore - English
	{207, "es"},//Honduras - Spanish
	{208, "es"},//Nicaragua - Spanish
	{209, "es"},//Puerto Rico - Spanish
	{210, "es"},//United States - Spanish
	{211, "en"},//Scotland - English
	{212, "zh"}//Hong Kong S.A.R - Chinese (Simplified)
};

public Dictionary<string, string> Formats = new Dictionary<string, string> 
{
	{"en", "MM/dd/yyyy" },
	{"ar", "dd/MM/yyyy" },
	{"cs", "dd.MM.yyyy" },
	{"da", "dd/MM/yyyy" },
	{"de", "dd.MM.yyyy" },
	{"es", "dd/MM/yyyy" },
	{"fi", "dd.MM.yyyy" },
	{"fr", "dd/MM/yyyy" },
	{"hu", "yyyy-MM-dd" },
	{"it", "dd/MM/yyyy" },
	{"nl", "dd-MM-yyyy" },
	{"pl", "yyyy-MM-dd" },
	{"pt", "dd/MM/yyyy" },
	{"ro", "dd.MM.yyyy" },
	{"ru", "dd.MM.yyyy" },
	{"si", "yyyy-mm-dd" },
	{"sk", "dd.MM.yyyy" },
	{"sv", "yyyy-MM-dd" },
	{"th", "dd/MM/yyyy" },
	{"tr", "dd.MM.yyyy" },
	{"uk", "dd.MM.yyyy" },
	{"zh", "yyyy-MM-dd" }
};