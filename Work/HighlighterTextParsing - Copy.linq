<Query Kind="Program" />

void Main()
{
	var slides = new List<string>
	{
		slide1 ,
		slide2 ,
		slide3 ,
		slide4 ,
		slide5 ,
		slide6 ,
		slide7 ,
		slide8 ,
		slide9 ,
		 slide10,
		 slide11,
		 slide12
	};
	int slideNum = 1;
	var text = slides.ToDictionary(s => 
		"slide" + slideNum++,
		s =>
		string.Join(
		'|', 
		Regex.Matches(s, @"{.*?}")
		.Select(m => 
			m.Value
			.Replace("{", "")
			.Replace("}", "<br>")))
	);
	text.Dump();
}

// You can define other methods, fields, classes and namespaces here
public string slide1 = @"
			<div class='slide1'>
			<div class='logo-header'><img src='https://s.gongos.com/bcki/media/slide1/logo.jpg'></div>

			<div class='slide1-first-div'>
				<table style='width:100%;'>
					<tr>
					<td style='width:60%; padding-left:25px;'>{It’s time to make<br> your first<br> payment}</td>
					<td style='width:40%;'><img src='https://s.gongos.com/bcki/media/slide1/heart.jpg'></td>
					</tr>
				</table>
			</div>

			<div class='slide1-second-div'>
				{Easy ways to pay:}
			</div>
			<div class='slide1-third-div'>
				<table style='width:100%;'>
					<tr>
					<td style='width:50%; vertical-align: top;'><img src='https://s.gongos.com/bcki/media/slide1/computer.jpg'><br/>{Online} {is the fastest,} {easiest} {way <br>to pay}</td>
					<td style='width:50%; vertical-align: top;'><img src='https://s.gongos.com/bcki/media/slide1/phone.jpg'><br/>{Call 1-888-200-0327,} {TTY 711} <br>{toll-free 24/7} {and press 1} {to make <br>a secure payment by phone.}</td>					
					</tr>
					<tr>
					<td colspan='2'><div style='background-color:#D35439; color:white; border-radius: 25px; height:40px; width:140px; margin-top:30px; margin-left:auto; margin-right:auto; padding:10px;'>{Pay Online Now}</div></td>
					</tr>
				</table>
			</div>

			<div class='slide1-fourth-div'>
				<div style='text-align:center; font-size:18px;'>{Questions?}</div><br/>
				<div>{Our experienced team} {is here to help.} {Call Member Services} {at <br>1-888-200-0327,} {TTY 711,} {8a.m.-6.pm.} {Monday-Friday,} {for help accessing the member information needed to make your payment.}<br/><br/>{Due to possible delays in receiving data,} {this message may not reflect payments made within the last 3 days.}</div>
			</div>

			<div class='slide1-fifth-div'>
				{myuhc.com/exchange}
			</div>

			</div>";



			public string slide2 = @"
			<div class='slide2'>
			<div class='info-header'>{Instant access} {to your online account.}<b><u> {Sign in now.}</u></b></u></div>
			<div class='logo-header' style='padding-top:10px;padding-bottom:10px;'><img src='https://s.gongos.com/bcki/media/slide2/logo.jpg'></div>

			<div class='slide2-first-div'>
				<table style='width:100%;'>
					<tr>
					<td style='width:50%; padding-left:25px; text-align:center;'>{Thanks for <br>choosing <br>UnitedHealthcare}</td>
					<td style='width:20%;'>&nbsp;</td>
					<td style='width:30%;'><img style='width:100%;' src='https://s.gongos.com/bcki/media/slide2/thumbsup.jpg'></td>
					</tr>
				</table>
			</div>
			<div class='slide2-second-div'>
				{Your new benefits} {are just around the corner}
			</div>
			<div class='slide2-third-div'>
				<table style='width:100%;'>
					<tr>
					<td colspan='2' style='text-align:center; font-size:16px; font-weight:bold; padding-bottom:15px;'>{How to get the most from your plan}</td>
					</tr>
					<tr>
					<td style='width:10%; padding-bottom:10px;'><div style='background-color:#384276; font-size:32px; color:white; width:45px; height:45px; border-radius:50%; text-align:center;'>1</div></td>
					<td style='width:90%; padding-bottom:10px; padding-left:10px;'><b>{Submit your first payment} {to complete your enrollment}</b><br/>{You’ll get a mailed invoice} {in the next few days.} {Use the member ID information on it} {to make your payment.}</td>
					</tr>
					<tr>
					<td style='width:10%; padding-bottom:10px;'><div style='background-color:#384276; font-size:32px; color:white; width:45px; height:45px; border-radius:50%; text-align:center;'>2</div></td>
					<td style='width:90%; padding-bottom:10px; padding-left:10px;'><b>{Read through your welcome kit} {and health plan ID card}</b><br/>{After you complete your payment,} {you’ll see your welcome kit} {and ID card} {in your mailbox} {within 2-3 weeks.}</td>
					</tr>
					<tr>
					<td style='width:10%; padding-bottom:10px;'><div style='background-color:#384276; font-size:32px; color:white; width:45px; height:45px; border-radius:50%; text-align:center;'>3</div></td>
					<td style='width:90%; padding-bottom:10px; padding-left:10px;'><b>{Register for your online account at} {myuhc.com/exchange}</b><br/>{It has all the tools} {you’ll need} {to use your new plan.}</td>
					</tr>
					<tr>
					<td colspan='2'><div style='background-color:#D35439; text-align:center; color:white; border-radius: 25px; height:40px; width:110px; margin-top:30px; margin-left:auto; margin-right:auto; padding:10px;'>{Pay now}</div></td>
					</tr>
				</table>
			</div>

			<div class='slide2-fourth-div'>
				{We’re excited to have you as a member.} {When you count on UnitedHealthcare®,} {you can count on more} {– and that’s exactly what we strive to deliver – every day.}
			</div>

			<div class='slide2-fifth-div'>
				{myuhc.com/exchange}
			</div>

			</div>";


			public string slide3 = @"
			<div class='slide3'>
				<div class='info-header'>{Instant access to your online account.} <u>{Sign In now.}</u></div>
				<div class='logo-header'><img src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide3/logo.jpg'></div>

				<div class='slide3-first-div'>
					<table style='width:100%;'>
						<tr>
							<td style='width:50%; padding-left:25px; text-align:center;'>{Get more from<br/>your health plan}</td>
							<td style='width:50%;'><img style='height:100%;' src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide3/getmorefromyourplan.JPG'></td>
						</tr>
					</table>
				</div>

				<div class='slide3-second-div'>
					<table style='margin-left:auto; margin-right:auto; width:90%;'>
						<tr>
							<td style='width:50%; vertical-align: top;'><h4><strong><b>{Welcome}</b></strong></h4></td>
							<td rowspan='2' style='width:50%; vertical-align: top;'><img src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide3/videoplayer.png'><br/></td>					
						</tr>
						<tr>
							<td style='width:50%; color:black; font-size: 12px; vertical-align: top;'>{Your new plan from<br/>UnitedHealthcare® is ready to go.}<br/>{You now have lots of ways to get<br/>quality care, including} {24/7 access}<br/>{to virtual care,} {and benefits} {that<br/>help you get} {and stay healthy.}</td>
						</tr>
						<tr>
							<td colspan='2'><div style='background-color:#D35439; color:white; border-radius: 25px; height:45px; width:140px; margin-top:15px; margin-left:auto; margin-right:auto; padding:10px; text-align:center;'>{Watch Video}</div></td>
						</tr>
					</table>
				</div>
				<div class='slide3-third-div'>
					<table style='width:100%;'>
						<tr>
							<td colspan='2' style='width:100%;'><h4><strong><b>{Access your plan anywhere, anytime}</b></strong></h4></td>						
						</tr>
						<tr>
							<td colspan='2' style='width:100%;'>{The easiest way to learn about your coverage} {and manage your plan} {is<br/>through your online account.} {If you haven’t already, register now at<br/><u>myuhc.com/exchange</u>.} {With your online account you can do things like:}<br/></td>
						</td>
					</table>
					<table style='width:100%;'>
						<tr>
							<td colspan='1' style='padding-left:9px;'><img src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide3/stethiscope.PNG'></td>
							<td colspan='4'>{Meet your primary care provider (PCP)} {– the doctor you’ll<br/>see first} {for care} {and referrals}</td>
						</tr>
						<tr>
							<td colspan='1' style='padding-left:14px;'><img src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide3/shoe.PNG'></td>
							<td colspan='4'>{Get your code to access} {the Peloton® app} {at no<br/>additional cost}</td>					
						</tr>
						<tr>
							<td colspan='1' style='padding-left:10px;'><img src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide3/medicalfile.PNG'></td>
							<td colspan='4'>{Print a copy of your health plan ID card}</td>
						</tr>
						<tr>
							<td colspan='1' style='padding-left:10px;'><img src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide3/phone.PNG'></td>
							<td colspan='4'>{Choose how we connect with you:} {email,} {text} {and mail}</td>
						</tr>
						<tr>
							<td colspan='1' style='padding-left:12px;'><img src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide3/rx.PNG'></td>
							<td colspan='4'>{Find network pharmacies} {and medication costs}</td>
						</tr>
						<tr>
							<td colspan='5'><div style='background-color:white; border:2px solid #384276; color:#384276; border-radius: 25px; font-size:16px; height:45px; width:100px; margin-bottom:20px; margin-top:10px; margin-left:auto; margin-right:42%; padding:10px; text-align:center;'>{Sign in}</div></td>
						</tr>
					</table>
				</div>

				<div class='slide3-fourth-div'>
					<div style='text-align:center; font-size:18px;'><h4><strong><b>{Questions?}</b></strong></h4></div>
					<div>{Our experienced team} {is here to help.} {Visit  myuhc.com/exchange} {or call<br/>1-888-200-0327,} {TTY 711}.</div>
				</div>

				<div class='slide3-fifth-div'>
					{myuhc.com/exchange}
				</div>
			</div>";




			public string slide4 = @"
			<div class='slide4'>
			<div class='slide4-first-div'>
				<p style='font-size:22px; font-weight:bold; padding-top:30px; padding-left:20px; margin-bottom:0px;'>{Thanks for choosing UnitedHealthcare}</p><br/><p style='font-size:12px; padding-left:20px; padding-right:10px;'>{There’s a lot to know about your health plan.} {This Quick Start Guide} {gives you tips} {for using your plan} {– like setting up your personal online account} {and finding care within your network.}</p><br/><p style='font-size:14px; padding-left:20px; padding-right:10px; margin-bottom:0px;'>{Questions?}</p><br/><p style='font-size:12px; padding-left:20px; padding-right:10px; margin-bottom:0px; margin-top:-20px;'>{Call the number on your health plan ID card.}</p><br/><img src='https://s.gongos.com/bcki/media/slide4/hands.jpg'>
			</div>
			<div class='slide4-second-div'>
				<img src='https://s.gongos.com/bcki/media/slide4/stethoscope.jpg'><br/><br/><p style='font-size:22px; color:#384276; font-weight:bold; padding-top:30px; padding-left:20px; margin-bottom:0px;'>{Welcome to UnitedHealthcare}</p><br/><p style='font-size:16px; color:#384276; font-weight:bold; padding-top:30px; padding-left:20px; margin-bottom:0px; margin-top:-30px;'>{Your 2022 Quick Start Guide}</p><br/><img style='float:right; margin-top:80px;' src='https://s.gongos.com/bcki/media/slide4/blueLogo.jpg'>
			</div>

			</div>";



			public string slide5 = @"
			<div class='slide5'>
			<div class='info-header'>{Get started} {with the Galileo app now.}<b><u>{Download app.}</u></b></u></div>
			<div class='logo-header' style='padding-top:10px;padding-bottom:10px;'><img src='https://s.gongos.com/bcki/media/slide5/logo.jpg'><img src='https://s.gongos.com/bcki/media/slide5/galileo.jpg'></div>

			<div class='slide5-first-div'>
				<table style='width:100%;'>
					<tr>
					<td style='width:70%; padding-left:30px;'>{Welcome to your <br>Virtual First health plan}</td>
					<td style='width:30%;'><img style='width:100%;' src='https://s.gongos.com/bcki/media/slide5/phones.jpg'></td>
					</tr>
				</table>
			</div>
			<div class='slide5-second-div'>
				<table>
					<tr>
					<td style='width:50%; vertical-align: top;'><img style='width:95%;' src='https://s.gongos.com/bcki/media/slide5/whiteVideo.jpg'></td>
					<td style='width:50%;'>{Your new Virtual First health plan from UnitedHealthcare®} {is now active.} {Through our partnership with Galileo,} {you have quick access} {to quality doctors} {and care for everything} {from everyday issues} {to complex conditions.} {And it's all available} {in the palm of your hand} {with an easy-to use} {smartphone app}{It’s a virtual first health plan} {that puts real people} { – like you –} {first.}
					<div style='background-color:#D35439; text-align:center; color:white; border-radius: 25px; height:40px; width:130px; margin-top:30px; margin-left:auto; margin-right:auto; padding:10px; font-size:14px;'>{Watch video}</div>	</td>
					</tr>
				</table>
			</div>
			<div class='slide5-third-div'>
				<table style='width:100%;'>
					<tr>
					<td style='text-align:center; font-size:16px; font-weight:bold; padding-bottom:15px;'><img src='https://s.gongos.com/bcki/media/slide5/phoneLightBlue.jpg'></td>
					</tr>
					<tr>
					<td><p style='font-size:14px; font-weight:bold; margin-bottom:0;'>{Want help getting set up?}</p><br/>{You can request} {a welcome call} {from our Member Services team} {to walk you through} {your new Virtual First plan.} {They’ll answer questions about your coverage,} {and help you get started with the Galileo app} {and your online account.}</td>
					</tr>
					<tr>
					<td style='text-align:center; padding-bottom:8px;'><div style='background-color:#384276; text-align:center; color:white; border-radius: 25px; height:35px; width:130px; margin-top:30px; margin-left:auto; margin-right:auto; padding:10px; font-size:12px;'>{Request a call}</div></td>
					</tr>
				</table>
			</div>

			<div class='slide5-fourth-div'>
				<p style='font-size:14px; font-weight:bold; margin-bottom:0; color:#384276'>{Meet Galileo} {your $0} {virtual care provider}</p><br/>{Galileo’s team of experts} {offer the same} {trusted,} {quality care} {you expect} {from regular visits to the doctor} {– now on your smartphone.} {Galileo is} {your virtual provider} {and first stop} {when you need care.} {If you haven’t already,} {create your account} {and download the app to get started.}<br/>
				<div style='background-color:#384276; text-align:center; color:white; border-radius: 25px; height:35px; width:200px; margin-top:30px; margin-left:auto; margin-right:auto; padding:10px; font-size:12px;'>{Get Started with Galileo}</div>
			</div>

			<div class='slide5-fifth-div'>
				<table style='width:100%;'>
					<tr>
					<td colspan='2' style='padding-bottom:10px;'><p style='font-size:14px; font-weight:bold; margin-bottom:0;'>{Access your plan online anytime, anywhere}</p><br/>{Your UnitedHealthcare online account is separate from your Galileo account.} {Use your myuhc.com/exchange account to:}</td>
					</tr>
					<tr>
					<td style='width:15%; text-align:right;'><img src='https://s.gongos.com/bcki/media/slide5/shoeLightBlue.jpg'></td>
					<td style='width:85%; padding-left:5px;'>{Get your code to access} {the Peloton® app} {at no additional cost}</td>
					</tr>
					<tr>
					<td style='width:15%; text-align:right;'><img src='https://s.gongos.com/bcki/media/slide5/cardLightBlue.jpg'></td>
					<td style='width:85%; padding-left:5px;'>{Print a copy of your health plan ID card}</td>
					</tr>
					<tr>
					<td style='width:15%; text-align:right;'><img src='https://s.gongos.com/bcki/media/slide5/smartPhoneLightBlue.jpg'></td>
					<td style='width:85%; padding-left:5px;'>{Choose how we connect with you:} {email,} {text} {and mail}</td>
					</tr>
					<tr>
					<td style='width:15%; text-align:right;'><img src='https://s.gongos.com/bcki/media/slide5/inNetworkLightBlue.jpg'></td>
					<td style='width:85%; padding-left:5px;'>{Find network pharmacies} {and price medications}</td>
					</tr>
					<tr>
					<td colspan='2' style='text-align:center; padding-bottom:8px;'><div style='background-color:white; border-color:#384276; border-style:solid; border-width:1px !important; text-align:center; color:#384276; border-radius: 25px; height:35px; width:230px; margin-top:30px; margin-left:auto; margin-right:auto; padding:10px; font-size:12px;'>{Access your online account}</div></td>
					</tr>
				</table>
			</div>

			<div class='slide5-sixth-div'>
				<p style='font-size:14px; font-weight:bold; margin-bottom:0; color:#384276'>{Questions?}</p><br/>{Our experienced team is here to help.} {Visit myuhc.com/exchange} {or call 1-877-482-9045,} {TTY 711}
			</div>

			<div class='slide5-seventh-div'>
				{myuhc.com/exchange}
			</div>

			</div>";




			public string slide6 = @"
			<div class='slide6'>
				<div class='info-header'>{Instant access} {to your online account.} <u>{Sign in now.}</u></div>
				<div style='background-color:white; border:1px; height:80px; padding-left:10px; padding-right:10px; padding-top:10px; width:100%;'>
					<div style='float:left; width:35%; height:80px;'><img src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide6/logo.jpg'></div>
					<div style='float:left; width:65%;'><img style='height:55px;' src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide6/galileo.JPG'></div>
				</div>
				<div class='slide6-first-div'>
					<table style='width:100%;'>
						<tr>
						<td style='width:50%; padding-left:25px; text-align:center;'>{Thanks for choosing<br/>a Virtual First<br/>plan with<br/>UnitedHealthcare}</td>
						<td style='width:20%;'>&nbsp;</td>
						<td style='width:30%;'><img style='width:100%;' src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide6/thumbsup.jpg'></td>
						</tr>
					</table>
				</div>
				<div class='slide6-second-div'>
					{Your new benefits} {are just around the corner}
				</div>
				<div class='slide6-third-div'>
					{Your new health plan will include} {unlimited, 24/7 access} {to $0 virtual care}<br/>{through the Galileo app.}
				</div>
				<div class='slide6-fourth-div'>
					<table style='width:100%;'>
						<tr>
							<td colspan='2' style='text-align:center; font-size:16px; font-weight:bold; padding-bottom:15px;'>{Get the most form your plan}</td>
						</tr>
						<tr>
							<td style='width:10%; padding-bottom:10px;'><div style='background-color:#384276; font-size:32px; color:white; width:45px; height:45px; border-radius:50%; text-align:center;'>1</div></td>
							<td style='width:90%; font-weight:normal; padding-bottom:10px; padding-left:10px;'><b>{Submit your first payment} {to complete your enrollment}</b><br/>{You'll get a mailed invoice} {in the next few days}. {Use the member ID information on it} {to make your payment}.</td>
						</tr>
						<tr>
							<td style='width:10%; padding-bottom:10px;'><div style='background-color:#384276; font-size:32px; color:white; width:45px; height:45px; border-radius:50%; text-align:center;'>2</div></td>
							<td style='width:90%; font-weight:normal; padding-bottom:10px; padding-left:10px;'><b>{Read through your welcome kit} {and the health plan ID card}</b><br/>{After you complete your payment,} {you’ll see your welcome<br/>kit and ID card in your mailbox} {within 2-3 weeks.}</td>
						</tr>
						<tr>
							<td style='width:10%; padding-bottom:10px;'><div style='background-color:#384276; font-size:32px; color:white; width:45px; height:45px; border-radius:50%; text-align:center;'>3</div></td>
							<td style='width:90%; font-weight:normal; padding-bottom:10px; padding-left:10px;'><b>{Set up your Galileo account}</b><br/>{Get started at galileohealth.com/united} {where you can<br/>create an account} {and download the app.}</td>
						</tr>
						<tr>
							<td style='width:10%; padding-bottom:10px;'><div style='background-color:#384276; font-size:32px; color:white; width:45px; height:45px; border-radius:50%; text-align:center;'>4</div></td>
							<td style='width:90%; font-weight:normal; padding-bottom:10px; padding-left:10px;'><b>{Register for your UnitedHealthcare online account}</b><br/>{Get started at myuhc.com/exchange to see} {your plan<br/>details,} {set up auto-pay,} {and more.}</td>
						</tr>
						<tr>
							<td colspan='2'><div style='background-color:#D35439; text-align:center; color:white; border-radius: 25px; height:40px; width:110px; margin-top:30px; margin-left:auto; margin-right:auto; padding:10px;'>{Pay now}</div></td>
						</tr>
					</table>
				</div>

				<div class='slide6-fifth-div'>
					{We’re excited to have you as a member.} {When you count on<br/>UnitedHealthcare,} {you can count on more} {– and that’s exactly what we<br/>strive to deliver every day.}
				</div>

				<div class='slide6-sixth-div'>
					{myuhc.com/exchange}
				</div>
			</div>";




			public string slide7 = @"
			<div class='slide7'>
			<div class='info-header'>{Instant access} {to your online account.}<b><u> {Sign in now.}</u></b></u></div>
			<div class='logo-header' style='padding-top:10px;padding-bottom:10px;'><img style='width:60%;' src='https://s.gongos.com/bcki/media/slide7/logo.jpg'></div>

			<div class='slide7-first-div'>
				<table style='width:100%;'>
					<tr>
					<td style='width:50%; padding-left:30px;'>{Your Virtual First health plan} {is <br>almost here}</td>
					<td style='width:50%;'><img style='width:100%;' src='https://s.gongos.com/bcki/media/slide7/computer.jpg'></td>
					</tr>
				</table>
			</div>
			<div class='slide7-second-div'>
				<table>
					<tr>
					<td>
						<p style='color:#384276; font-size:20px; font-weight:bold;'>{Let’s get started with your Virtual First plan}</p>
					</td>
					</tr>
					<tr>
					<td style='font-size:13px;'>
						{Once your coverage begins,} {you’ll get $0 unlimited,} {24/7 access to care} {through the Galileo app.}<br /><br /> {All you have to do is} {create your Galileo account} {and download the Galileo app.} {You’ll need your member ID to create your account.} {You can find your member ID} {on your health plan ID card} {or on your mailed invoice.}
					</td>
					</tr>
					<tr>
					<td style='font-size:13px;'>
						<div style='background-color:#D35439; text-align:center; color:white; border-radius: 25px; height:40px; width:130px; margin-top:10px; margin-left:auto; margin-right:auto; padding:10px; font-size:14px;'>{Get started}</div>	</td>
					</td>
					</tr>
				</table>
			</div>
			<div class='slide7-third-div'>
				<table style='width:100%;'>
					<tr>
					<td style='width:55%;'><p style='font-size:14px; font-weight:bold; margin-bottom:0;'>{Get to know Galileo}</p><br/>{A team of expert doctors is waiting for you} {in the Galileo app.} {They’re here to support} {your virtual care needs} {from primary} {to specialty} {and beyond.} {Watch this video to learn more about} {the convenience of getting care anywhere you have your smart phone.}</td>
					<td style='width:45%;'><img src='https://s.gongos.com/bcki/media/slide7/phones.jpg'></td>
					</tr>
					<tr>
					<td colspan='2' style='text-align:center; padding-bottom:8px;'><div style='background-color:#384276; text-align:center; color:white; border-radius: 25px; height:35px; width:130px; margin-top:30px; margin-left:auto; margin-right:auto; padding:10px; font-size:12px;'>{Watch video}</div></td>
					</tr>
				</table>
			</div>

			<div class='slide7-fourth-div'>
				<p style='font-size:14px; font-weight:bold; margin-bottom:-17px; color:#384276'>{Manage your plan anywhere, anytime}</p><br/>{Register your UnitedHealthcare online account with} {your member ID} {and group number} {at myuhc.com/exchange.} {With your online account you can do things like:}<br/><br/>
				<div><img style='width:15%; padding-left:30px;' src='https://s.gongos.com/bcki/media/slide7/idCard.jpg'> {View your ID card}<br /><img style='width:15%; padding-left:30px;' src='https://s.gongos.com/bcki/media/slide7/phone.jpg'> {Choose how we connect with you:} {email,} {text} {and mail}</div>
				<td colspan='2' style='text-align:center; padding-bottom:8px;'<br /><br /><div style='background-color:white; border-color:#384276; border-style:solid; border-width:2px !important; text-align:center; color:#384276; border-radius: 25px; height:35px; width:130px; margin-top:10px; margin-left:auto; margin-right:auto; padding:10px; font-size:12px; font-weight:bold;'>{Register now}</div></td>
			</div>

			<div class='slide7-fifth-div'>
				<p style='font-size:14px; font-weight:bold; margin-bottom:0; color:#384276'>{Questions?}</p><br/>{Our experienced team} {is here to help.} {Visit myuhc.com/exchange} {or call 1-877-482-9045,} {TTY 711}
			</div>

			<div class='slide7-sixth-div'>
				{myuhc.com/exchange}
			</div>

			</div>";


			public string slide8 = @"
			<div class='slide8'>
			<div class='slide8-first-div'>
				<p style='font-size:22px; font-weight:bold; padding-top:30px; padding-left:20px; margin-bottom:0px;'>{Thanks for choosing UnitedHealthcare}</p><br/><p style='font-size:12px; padding-left:20px; padding-right:10px;'>{Your Virtual First plan includes} {24/7} {$0 unlimited access} {to quality virtual care} {through the Galileo app.} {Get treated for anything} {from everyday issues} {to complex conditions} {– plus, Galileo’s app enables patients and providers to stay in touch,} {so care doesn’t stop} {when the consultation ends.}</p><br/><p style='font-size:14px; padding-left:20px; padding-right:10px; margin-bottom:0px;'>{Questions?}</p><br/><p style='font-size:12px; padding-left:20px; padding-right:10px; margin-bottom:0px; margin-top:-20px;'>{Get started at} {galileohealth.com/united} {or call the number on your ID card.}</p><br/><img src='https://s.gongos.com/bcki/media/slide8/hands.jpg'>
			</div>
			<div class='slide8-second-div'>
				<img src='https://s.gongos.com/bcki/media/slide8/stethoscope.jpg'><br/><br/><p style='font-size:22px; color:#384276; font-weight:bold; padding-top:30px; padding-left:20px; margin-bottom:0px;'>{Welcome to UnitedHealthcare}</p><br/><p style='font-size:16px; color:#384276; font-weight:bold; padding-top:30px; padding-left:20px; margin-bottom:0px; margin-top:-30px;'>{Your 2022 Quick Start Guide}</p><br/><img style='float:right; margin-top:80px; width:75%;' src='https://s.gongos.com/bcki/media/slide8/blueLogo.jpg'>
			</div>

			</div>";
public string slide9 = @"
			<div class='slide9'>
			<div class='info-header'>{Instant access} {to your online account.} <u>{Sign in now.}</u></div>
			<div class='logo-header' style='padding-top:10px;padding-bottom:10px;'><img src='https://s.gongos.com/bcki/media/slide9/logo.jpg'></div>

			<div class='slide9-first-div'>
				<table style='width:100%;'>
					<tr>
					<td style='width:50%; padding-left:25px; text-align:center;'>{When you count on UnitedHealthcare®,} {<br>you can count on more}</td>
					<td style='width:10%;'>&nbsp;</td>
					<td style='width:40%;'><img style='width:100%;' src='https://s.gongos.com/bcki/media/slide9/heart.jpg'></td>
					</tr>
				</table>
			</div>
			<div class='slide9-second-div'>
				<p style='color:#384276; font-size:18px; padding-left:25px; padding-right:25px; text-align:center;'>{Your plan comes with great benefits} {and even more} {are on the way}</p>
				<br />
				<p style='padding-left:15px; padding-right:15px; margin-top:-10px;'>{Thanks for being a UnitedHealthcare® member.} {If you’d like to stay with your current plan,} {you don’t have to do anything extra} {to continue} {with your coverage into next year.} {Just keep paying your monthly premiums.} {If your household} {or income information} {has changed,} {give us a call at 1-877-608-3512} {or contact your broker} {to update your information,} {make sure} {you’re in the best plan for you,} {and see if you qualify} {for lower premiums.}</p>
			</div>
			<div class='slide9-third-div'>
				<table style='width:100%;'>
					<tr>
					<td colspan='2' style='text-align:center; font-size:16px; font-weight:bold; padding-bottom:15px;'>{Look forward} {to more in 2022:}</td>
					</tr>
					<tr>
					<td style='width:15%; padding-bottom:10px; padding-right:5px;'><div style='font-size:30px; color:#384276; text-align:right;'>1</div></td>
					<td style='width:85%; padding-bottom:10px;'><b>{More ways} {to access care online,} {including virtual primary care}</b></td>
					</tr>
					<tr>
					<td style='width:15%; padding-bottom:10px; padding-right:5px;'><div style='font-size:30px; color:#384276; text-align:right;'>2</div></td>
					<td style='width:85%; padding-bottom:10px;'><b>{Easier access} {to see network specialists}</b></td>
					</tr>
					<tr>
					<td style='width:15%; padding-bottom:10px; padding-right:5px;'><div style='font-size:30px; color:#384276; text-align:right;'>3</div></td>
					<td style='width:85%; padding-bottom:10px;'><b>{Rewards} {for doing activities that keep you healthy}</b></td>
					</tr>
					<tr>
					<td style='width:15%; padding-bottom:10px; padding-right:5px;'><div style='font-size:30px; color:#384276; text-align:right;'>4</div></td>
					<td style='width:85%; padding-bottom:10px;'><b>{Access to} {the Peloton® App} {at no additional cost}</b></td>
					</tr>
					<tr>
					<td style='width:15%; padding-bottom:10px; padding-right:5px;'><div style='font-size:30px; color:#384276; text-align:right;'>5</div></td>
					<td style='width:85%; padding-bottom:10px;'><b>{Expanded} {provider network coverage}</b></td>
					</tr>
				</table>
			</div>

			<div class='slide9-fourth-div'>
				<p style='color:#384276; font-size:16px;'>{Interested in shopping} {for other UnitedHealthcare plans} {in your area?}</p>
				<br />
				<p style='margin-top:-20px;'>{Head to uhcexchange.com/yourplan} {or call 1-877-608-3512 TTY 711.}</p>
				<br />
				<div style='background-color:#384276; text-align:center; color:white; border-radius: 25px; height:35px; width:130px; margin-top:10px; margin-left:auto; margin-right:auto; padding:10px; font-size:12px;'>{Learn More}</div>
			</div>

			<div class='slide9-fifth-div'>
				{myuhc.com/exchange}
			</div>

			</div>";




			public string slide10 = @"
			<div class='slide10'>
			<table>
				<tr>
				<td class='slide10TD'>{UnitedHealthcare:} {Hi, (FIRSTNAME).} {There’s nothing you need to do to} {stay enrolled for next year.} {We’ve got you covered}! {Learn more at https://uhc.care/yourplan} {reply HELP for help,} {STOP to cancel.}</td>
				<td class='slide10TD'>{UnitedHealthcare:} {Hi, (FIRSTNAME).} {Have you seen} {some of your new benefits} {for next year?} { (They’re pretty great.)} {Check them out at https://uhc.care/2022} {reply HELP for help,} {STOP to cancel.}</td>
				<td class='slide10TD'>{UHC:} {Good news (FIRSTNAME),} {your monthly premium} {is estimated} {to be lower in 2022.} {Stick with us} {next year} {and save.} {Learn more at https://uhc.care/myplan} {Reply HELP for help,} {STOP to cancel.}</td>
				</tr>
			</table>

			</div>";




			public string slide11 = @"
			<div class='slide11'>
				<div class='slide11-first-div'>
					<div style='background-color:#243471; float:left; height:400px; padding:18px; width:31%;'>
						<div><img style='margin-right:10px' src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide11/bluelogo.JPG'></div>
						<div style='font-size:16px; padding-top:10px; padding-bottom:10px;'><strong><b>{Health care<br/>at 2 a.m.}</b></strong></div>
						<div><b>{Another reason} {to register<br/>your UnitedHealthcare<br/>online account}</b></div>
						<div style='background-color:#D35439; text-align:center; border-radius: 25px; height:35px; width:100px; margin-top:35px; padding:10px;'>{Visit us again}</div>
						<div><img style='margin-top:45px;' src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide11/sleep.JPG'></div>
					</div>
					<div style='background-color:#243471; float:left; height:400px; padding:18px; margin-left:15px; width:31%;'>
						<div><img src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide11/bluelogo.JPG'></div>
						<div style='font-size:16px; padding-top:10px; padding-bottom:10px;'><strong><b>{Registering} {is<br/>easy peasy</b>}</strong></div>
						<div><b>{Sign on to your<br/>UnitedHealthcare online<br/>account} {to access your<br/>plan} {24/7}</b></div>
						<div style='background-color:#D35439; text-align:center; border-radius: 25px; height:35px; width:100px; margin-top:20px; margin-right:auto; padding:10px;'>{Register now}</div>
						<div><img style='margin-top:45px;' src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide11/bluecomputer.JPG'></div>
					</div>
					<div style='background-color:#243471; float:right; height:400px; padding:18px; width:31%;'>
						<div><img src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide11/bluelogo.JPG'></div>
						<div style='font-size:16px; padding-top:10px; padding-bottom:10px;'><strong><b>{The answers you<br/>want} {are here}</b></strong></div>
						<div><b>{Register your<br/>UnitedHealthcare online<br/>account} {to learn about<br/>your benefits}</b></div>
						<div style='background-color:#D35439; text-align:center; border-radius: 25px; height:35px; width:100px; margin-top:20px; margin-right:auto; padding:10px;'>{Register now}</div>
						<div><img style='margin-top:20px;' src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide11/card.JPG'></div>
					</div>
				</div>
				<div class='slide11-second-div'>
					<div style='float:left; height:400px; padding:18px; width:31%;'>
						<div><img style='height:40px; width:100px;' src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide11/logo.jpg'></div>
						<div style='font-size:16px; padding-top:10px; padding-bottom:10px;'><strong><b>{In sickness<br/>and in health}</b></strong></div>
						<div><b>{Now is always a good<br/>time} {to visit your<br/>UnitedHealthcare<br/>online account</b></div>
						<div style='background-color:#D35439; color:white; text-align:center; border-radius: 25px; height:35px; width:100px; margin-top:20px; padding:10px;'>{Visit us again}</div>
						<div><img style='margin-top:10px;' src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide11/heartstethiscope.JPG'></div>
					</div>
					<div style='float:left; height:400px; padding:18px; margin-left:15px; width:31%;'>
						<div><img style='height:40px; width:100px;' src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide11/logo.jpg'></div>
						<div style='font-size:16px; padding-top:10px; padding-bottom:10px;'><strong><b>{Ready for} {here,}<br/>{there,} {anywhere}<br/>{care?}</b></strong></div>
						<div><b>{Visit your<br/>UnitedHealthcare online<br/>account} {for access} {to<br/>virtual care and more}</b></div>
						<div style='background-color:#D35439; color:white; text-align:center; border-radius: 25px; height:35px; width:100px; margin-top:10px; margin-right:auto; padding:10px;'>{Visit us again}</div>
						<div><img style='margin-top:10px;' src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide11/phoneapp.JPG'></div>
					</div>
					<div style='float:right; height:400px; padding:18px; width:31%;'>
						<div><img style='height:40px; width:100px;' src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide11/logo.jpg'></div>
						<div style='font-size:16px; padding-top:10px; padding-bottom:10px;'><strong><b>{Care to find in-<br/>network care?}</b></strong></div>
						<div><b>{Another reason to visit<br/>your UnitedHealthcare<br/>online account}<br/></b></div>
						<div style='background-color:#D35439; color:white; text-align:center; border-radius: 25px; height:35px; width:100px; margin-top:20px; margin-right:auto; padding:10px;'>{Visit us again}</div>
						<div><img style='margin-top:10px;' src='https://uhg211407ifpopenenrollmentandonboarding.instinct.gongos.com/file/surveymedia/1591/14009/slide11/sign.JPG'></div>
					</div>
				</div>
			</div>";





			public string slide12 = @"
			<div class='slide12'>
				<div class='slide12-first-div'>
					<table style='width:100%;'>
						<tr>
							<td rowspan='2' style='width:20%;'><img src='https://s.gongos.com/xivo/media/slide2/logo.jpg'></td>
							<td style='width:80%;'>{Questions?} {Call UnitedHealthcare,} {Individual} {and Family Plans}</td>
						</tr>
						<tr>
							<td style='width:80%; color:blue;'><u>{1-877-608-3512</u>,} {TTY 711}</td>
						</tr>
					</table>
				</div>
				<div class='slide12-second-div'>
					<table style='float:left; margin-top:20px; width:55%;'>
					<tbody>
						<tr>
							<td style='color:#384276; font-size:24px'><strong><b>{When you count on us,} {you can<br/>count on more}</b></strong></td>
						</tr>
						<tr>
							<td>{Thank you for being a UnitedHealthcare member.} {We're here to help you} {live your<br/>best life,} {and are committed} {to making your health insurance} {simple,} <br/>{affordable,} {and reliable.}</td>
						</tr>
						<tr>
							<td style='color:#384276; font-size:14px'><strong><b>{See what's new with your plan for next year}:</b></strong></td>
						</tr>
						<tr>
							<td class='bulletpoint'>{Get rewarded} {for completing certain health activities}</td>
						</tr>
						<tr>
							<td class='bulletpoint'>{You'll have access to} {the Peloton app} {at no additional cost}</td>
						</tr>
						<tr>
							<td style='color:#384276; font-size:14px'><strong><b>{Want to stay enrolled? It’s Easy.}</b></strong></td>
						</tr>
						<tr>
							<td class='bulletpoint'>{If you’d like to stay with your plan,} {you don’t have to do anything extra.} {We think<br/>your time is better spent doing things you love.} {Just keep paying your monthly<br/>premiums} {and your coverage will continue into next year}</td>
						</tr>
						<tr>
							<td class='bulletpoint'>{Looking for something new?} {If you’ve had a life or job change} {and want to see<br/>if you qualify} {for lower premiums} {or out-of-pocket costs}, {give us a call at 1-877-<br/>608-3512,} {TTY 711} {or compare your options.} </td>
						</tr>
						<tr>
							<td class='bulletpoint'>{Also, if you’ve had a change in your health,} {your life,} {or your job,} {it’s best to<br/>update} {your household} {or income information.} {Please visit HealthCare.gov} {or<br/>your state-based health insurance marketplace} {to update your application.}</td>
						</tr>
						<tr>
							<td style='color:#384276; font-size:14px'><strong><b>{Interested in shopping} {for plans} {in your area?} {Open Enrollment<br/>runs} {Nov 1 – Jan 15}</b></strong></td>
						</tr>
						<tr>
							<td><div style='background-color:#D35439; float:left; text-align:center; color:white; border-radius: 25px; height:40px; width:150px; margin-top:20px; margin-left:auto; margin-right:auto; padding:10px;'>{Compare your options}</div></td>
						</tr>
					</tbody>
					</table>
					<div style='float:right; width:45%;'><img src='https://s.gongos.com/bqj4/media/handheart.jpg'></div>
				</div>
				<div class='slide12-third-div'>
					<table style='width:100%;'>
						<tr>
							<td colspan='3' style='color:#384276; font-size:24px; padding-top:20px; padding-bottom:10px; text-align:center;'><strong><b>{To thank you for being a member,} {we are now introducing} {added<br/>discounts}</b></strong></td>
						</tr>
						<tr style='background-color:#384276; height:100px'>
							<td colspan='2'style='color:white; font-size:16px; padding-left:20px; width:70%;'><strong>{Thanks for being a UnitedHealthcare member.} {We think our members<br/>deserve the best,} {so you’ve got more than 200 discounts} {at your<br/>fingertips} {on things you’ll actually use,} {like what’s listed below.}</strong></td>
							<td style='width:30%;'><div style='background-color:white; text-align:center; color:#384276; border-radius: 25px; height:40px; width:200px; margin-top:5px; margin-left:auto; margin-right:auto; padding:10px;'>{See all your retail savings}</div></td>
						</tr>
					</table>
				</div>
				<div class='slide12-fourth-div'>
					<div style='border:2px outset #F0F0F0; float:left; height:380px; padding:18px; width:32%;'>
						<div><img src='https://s.gongos.com/bqj4/media/whiteeyetest.JPG'></div>
						<div style='color:#384276; font-size:14px; padding-top:10px; padding-bottom:10px;'><strong><b>{Savings to protect your<br/>vision}</b></strong></div>
						<div>{Get up to 35% saving}s{ on LASIK vision<br/>correction} {through QualSight® LASIK,} {20%<br/>off blue light screen filters} {and 10% off on<br/>contact lenses.}</div>
						<div style='background-color:#384276; text-align:center; color:white; border-radius: 25px; height:40px; width:200px; margin-top:100px; margin-left:auto; margin-right:auto; padding:10px;'>{See all your vision discounts}</div>
					</div>
					<div style='border:2px outset #F0F0F0; float:left; height:380px; padding:18px; margin-left:18px; width:32%;'>
						<div><img src='https://s.gongos.com/bqj4/media/whiteshoe.JPG'></div>
						<div style='color:#384276; font-size:14px; padding-top:10px; padding-bottom:10px;'><strong><b>{Fitness discounts} {that<br/>help your waist} {and wallet}</b></strong></div>
						<div>{You’ve got online coaching programs}<br/>{designed to help you} {lose weight,} {reduce<br/>stress,} {manage diabetes,} {quit tobacco,} {and<br/>more.} {Plus, discounts} {on health club<br/>memberships} {and personal training<br/>sessions.}</div>
						<div style='background-color:#384276; text-align:center; color:white; border-radius: 25px; height:40px; width:150px; margin-top:62px; margin-left:auto; margin-right:auto; padding:10px;'>{See all your savings}</div>
					</div>
					<div style='border:2px outset #F0F0F0; float:right; height:380px; padding:18px; width:32%;'>
						<div><img src='https://s.gongos.com/bqj4/media/whitephone.JPG'></div>
						<div style='color:#384276; font-size:14px; padding-top:10px; padding-bottom:10px;'><strong><b>{Discounts to keep you<br/>connnected}</b></strong></div>
						<div>{Plan a night out}<br/>{with friends and family} {at places like your favorite restaurant} {or local<br/>movie theater.} {Plus, get some of the latest<br/>products} {and technology} {from companies<br/>like} {Apple and Xfinity.} {And with up to 80%<br/>off} {on hearing aids,} {everyone will be able to<br/>enjoy the good conversation} {(or the movie<br/>screen)!</div>
						<div style='background-color:#384276; text-align:center; color:white; border-radius: 25px; height:40px; width:150px; margin-top:13px; margin-left:auto; margin-right:auto; padding:10px;'>{See all your savings}</div>
					</div>
				</div>
				<div class='slide12-fifth-div'>
					<div style='color:#384276; font-size:16px; text-align:center;'><strong><b>{Questions about your current plan?} {Call the number on your health plan ID card} {or log in to your online<br/>account at <u>myuhc.com/exchange</u>.}</b></strong></div>
				</div>
			</div>";