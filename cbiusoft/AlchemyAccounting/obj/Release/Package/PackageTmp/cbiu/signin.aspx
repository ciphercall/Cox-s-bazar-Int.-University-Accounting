<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signin.aspx.cs" Inherits="AlchemyAccounting.cbiu.signin" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <title>CBIU Soft</title>
    <meta name="generator" content="Bootply" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"> 
    <link href="/cbiu/css/bootstrap.min.css" rel="stylesheet">
    <link href="/cbiu/css/font-awesome.min.css" rel="stylesheet"> 
    <!--[if lt IE 9]>
      <script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <link href="/cbiu/css/styles.css" rel="stylesheet"> 
</head>

<body>
    <form runat="server">
    <nav class="navbar navbar-trans navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="navbar-header">
                <!--  <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar-collapsible"> -->
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>


                <a class="navbar-brand companyName" href="#">
                    <img src="/cbiu/images/logo.png" alt="">
                    Cox's Bazar International University</a>
            </div>
            <div class="navbar-collapse collapse pull-right" id="navbar-collapsible">
                <ul class="nav navbar-nav navbar-left mainNav">
                    <li><a href="#section1"><i class="fa fa-home"></i>Home</a></li>
                    <li><a href="#myTab"><i class="fa fa-user"></i>About Us</a></li>
                    <li><a href="#contact"><i class="fa fa-phone"></i>Contact</a></li> 
                    <%if (Session["UserName"]==null)
                      {%>
                    <li>
                        <a href="#" data-toggle="modal" data-target="#myModal">Login</a>

                    </li>
                    <%} else
                      {%>
                    <li>
                        <asp:LinkButton ID="btnLogOut" runat="server" Text="Log out" OnClick="btnLogOut_Click" />
                    </li>
                    <%} %>
                </ul>
            </div>
        </div>
    </nav>
    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content" style="height:195px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel">User login</h4>
                </div>
                <div class="modal-body">
                  
                        <div class="form-group">  
                            <asp:TextBox ID="txtUserName" CssClass="form-control" placeholder="User ID" runat="server" OnTextChanged="txtUserName_TextChanged"></asp:TextBox>
                        </div>
                        <div class="form-group">  
                            <%--<input type="password" class="form-control" id="exampleInputPassword3" placeholder="Password">--%>
                            <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" placeholder="Password" runat="server" OnTextChanged="txtPassword_TextChanged"></asp:TextBox>
                        </div> 
                        <asp:Button ID="btnLogIN" runat="server" CssClass="form-control-right" Text="Sign in" Width="100px" OnClick="btnLogIN_Click" />
                        <%--<button type="submit" class="btn btn-default">Sign in</button>--%>
                        <asp:Label ID="lblmsg" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        <asp:Label ID="lblLoginAs" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblClientName" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
                    
                </div>
                

            </div>
        </div>
    </div>
    <!-- model-->
    <section style="background: #1C539E" id="section1">
        <div class="row">
            <div class="container">
                <div class="col-md-12">
                    <img src="images/homeImage.png" alt="" style="display: block; margin: auto;">
                </div>
            </div>
        </div>
    </section>

    <section>
        <div class="row">
            <div class="container">
                <div class="col-md-12">
                    <br>
                    <h2 align="center" style="font-family: 'Bree Serif', serif;">Welcome to Cox's Bazar International University</h2>
                    <p align="justify" style="line-height: 30px;">Graced with natural beauty, Cox's Bazar is well known to all the people of the world. The longest motorable beach of the Bay of Bengal attracts thousands of curious tourists of home and abroad. Unfortunately, this popular and famous tourist city had not been graced with a university until now. So, the students of Cox's Bazar reluctantly had to go to Chittagong, Dhaka or elsewhere for higher studies incurring heavy financial loss for their accommodation, food and transportation, which left the students and guardians into a lasting anxiety and uncertainty.The people of Cox's Bazar had been in a dire need of a good university for qualitative higher education. Keeping these end in view, we strongly felt that we should establish a private university of international standard for imparting quality education to the students of Cox's Bazar. Therefore, we have decided to establish the Cox's Bazar International University to meet the needs of the inhabitants of this district. We expect, this university will attain a high standard of education in order to cope with the advancement of modern world.</p>
                    <br>
                    <ul class="nav nav-tabs" role="tablist" id="myTab">
                        <li role="presentation" class="active"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">University Profile</a></li>
                        <li role="presentation"><a href="#profile" aria-controls="profile" role="tab" data-toggle="tab">CBIU Campus</a></li>
                        <li role="presentation"><a href="#messages" aria-controls="messages" role="tab" data-toggle="tab">Mission and Vision</a></li>
                        <li role="presentation"><a href="#settings" aria-controls="settings" role="tab" data-toggle="tab">Academic Programs</a></li>
                    </ul>
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="home">
                            <br>
                            <strong>Name</strong> : Cox’s Bazar International University
                           
                            <br>
                            <strong>Mission</strong> : Committed to spreading quality education for students of CBIU by nurturing the creative, proactive, self-confidence spirit, looking forward, united and co-operative, skilled in own discipline, for students of so far less-privileged far flung areas and local students going to Chittagong or Dhaka facing hardships and incurring a heavy financial loss for maintaining residential accommodation, conveyance and food for a long period of time required to complete the undergraduate program.
                           
                            <br>
                            <strong>Acronym</strong> : CBIU
                           
                            <br>
                            <strong>Founded </strong>: September, 2013
                           
                            <br>
                            <strong>Type</strong> : Private University
                           
                            <br>
                            <strong>Chancellor</strong> : Abdul Hamid, Honorable President of the People’s Republic of Bangladesh.
                           
                            <br>
                            <strong>Location</strong> : Kolatoli Circle, Cox’s Bazar-4700
                           
                            <br>
                            <strong>Campus & Office </strong>: Dynamic Cox Kingdom (1st - 3rd Floor), Kolatoli Circle, Cox’s Bazar- 4700) of about 42000 sqft area.
                           
                            <br>
                            <strong>Accreditation</strong> : Ministry of Education, Gvot. of the People’s Republic of Bangladesh and University Grants Commission (UGC). Cox’s Bazar International University got the permission as a private university under the Private University Act 2010 from the Ministry of Education and UGC in September 2013. The academic programs have been started from January, 2014.
                           
                            <br>
                            <strong>Website</strong> : www.cbiu.ac.bd
                           
                            <br>
                            <strong>Email</strong> : cbiu.bd@gmail.com
                           
                            <br>
                            <strong>Cell</strong> : +88 01762686274-75, Tel: +88 0341-52510, Fax: +88 0341-52511
                       
                        </div>
                        <div role="tabpanel" class="tab-pane" id="profile">
                            <br>
                            The Cox's Bazar International University has started its function at the building located in the nerve center of Cox's Bazar- Kolatoli Circle, where appealing and exciting roaring of waives of the Bay of Bengal can be enjoyed before or after academic or official session. All amenities and facilities for higher education are available for students, faculty members, management and even visitors from home and abroad.
                           
                            <br>
                            Within a very reasonably short period, we will be able to start more programs with better and modern facilities in new own building at the adjoining place of this building.
                           
                            <p></p>
                            <img src="images/campus.png" alt="">
                        </div>
                        <div role="tabpanel" class="tab-pane" id="messages">
                            <br>
                            <strong>Mission</strong>
                            <br>
                            CBIU has embarked on mission to bring about a significant changes in the arena of higher education in Cox’s Bazar. CBIU is committed to imparting quality education to all students irrespective of family status and background inspiring them to develop their fullest potentials.
                           
                            <br>
                            Committed to spreading quality education for students of CBIU by nurturing the creative, proactive, self-confidence spirit, looking forward, united and co-operative, skilled in own discipline, for students of so far less-privileged far flung areas and local students going to Chittagong or Dhaka facing hardships and incurring a heavy financial loss for maintaining residential accommodation, conveyance and food for a long period of time required to complete the undergraduate program.
                           
                            <br>
                            <br>
                            <strong>Vision</strong>
                            <br>
                            To ensure dynamic teaching environment with accelerated success & prosperity in different fields conducive to enviable national development.
                       
                        </div>
                        <div role="tabpanel" class="tab-pane" id="settings">
                            <br>
                            <div class="col-md-4">
                                <p>Business Administration<span></span></p>
                                <p>Law<span></span></p>
                                <p>English<span></span></p>
                                <p>Computer Science & Engineering<span></span></p>
                                <p>Hospitality & Tourism Management<span></span></p>
                                <p>Library & Information Science<span></span></p>
                            </div>
                            <div class="col-md-4">
                                <p>MBA<span></span></p>
                                <p>EMBA<span></span></p>
                                <p>MA in English<span></span></p>
                                <p>LLB (Pass)<span></span></p>
                                <p>Diploma in Library & Information Science<span></span></p>
                            </div>
                            <div class="col-md-4">
                                <p>Diploma in Computer Science<span></span></p>
                                <p>Certificate course in English<span></span></p>
                                <p>Certificate course in CSE<span></span></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section class="row-fluid" id="contact" style="min-height: auto !important">
        <div class="container">
            <h1 class="text-center" style="">CONTACT</h1>
            <hr>
            <div class="row">
                <div class="col-sm-4 text-center">
                    <h3>Chittagong Info Office</h3>
                    <p>
                        R.F. Classic Point (1st Floor)
                       
                        <br>
                        8 Katalgonj Road,
                       
                        <br>
                        Chawkbazar, Chittagong.
                    </p>
                </div>
                <div class="col-sm-4 text-center">
                    <h3>Address</h3>
                    <p>
                        Dynamic Cox Kingdom
                       
                        <br>
                        Kolatoli Circle,
                       
                        <br>
                        Cox’s Bazar- 4700
                   
                    </p>
                </div>
                <div class="col-sm-4 text-center">
                    <h3>Contact us</h3>
                    <p>
                        Tel : +880 341 52510
                       
                        <br>
                        Cell: +880 1762686274-5
                       
                        <br>
                        Fax: +880 341 52511
                       
                        <br>
                        Email: cbiu.bd@gmail.com
                   
                    </p>
                </div>
                <div class="col-md-12">
                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d14857.368543875406!2d91.984395!3d21.415793!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0xb2d6f6784eeed39b!2sCox&#39;s+Bazar+International+University!5e0!3m2!1sen!2s!4v1426758766385" width="100%" height="450" frameborder="0" style="border: 0"></iframe>
                </div>
            </div>
        </div>
        <!--/row-->
    </section>
    <footer id="footer">
        <div class="container">
            <div class="row">
                <div class="col-xs-6 col-md-3">
                    <p><strong>Developed By : <a href="http://alchemy-bd.com" target="_blank" style="color: #F1C40F;">Alchemy Software</a></strong></p>

                </div>
                <div class="pull-right" style="margin-top: -9px;">
                    <span>Keep connected : </span><a href="https://www.facebook.com/cbiubd"><i class="fa fa-facebook-square fa-2x"></i></a>
                </div>
            </div>
            <!--/row-->
        </div>
    </footer>
    <!-- script references -->
    <script src="/cbiu/js/jquery-2.1.3.js"></script>
    <script src="/cbiu/js/bootstrap.min.js"></script>
    <script src="/cbiu/js/scripts.js"></script>
        </form>
</body>

</html>
