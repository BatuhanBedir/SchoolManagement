﻿const baseUrl = "https://localhost:7201/api/"


$(document).ready(function () {
    if (sessionStorage.getItem("token") != null) {
        $('#hello').show();
        if (window.location == 'https://localhost:7241/Home/Index') {
            $('#hello').html('<a class="nav-link text-dark">Welcome' + sessionStorage.getItem('role') + '</a>')
        }

        $('#logoutButton').show();
        $('#studentsButton').show();
        $('#registerButton').hide();
        $('#loginButton').hide();
    }
    else {
        $('#logoutButton').hide();
        $('#studentsButton').hide();
        $('#registerButton').show();
        $('#loginButton').show();
    }

    if (window.location == 'https://localhost:7241/Students') {
        GetStudentList();
    }
});

function GetStudentList() {
    var myUrl = baseUrl + "Students";
    $.ajax({
        url: myUrl,
        type: "GET",
        headers: {
            "Authorization": 'Bearer ' + sessionStorage.getItem("token")
        },
        success: function (response) {
            GetStudentListPartial(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    });
}

function GetStudentListPartial(students) {
    var myUrl = "/Students/GetStudentsListPartial";
    $.ajax({
        url: myUrl,
        type: "POST",
        data: students,
        success: function (response) {
            $("#letMeSee").html(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    });
}

function GetSchoolsList() {
    var myUrl = baseUrl + "Schools";
    $.ajax({
        url: myUrl,
        type: "GET",
        headers: {
            "Authorization": 'Bearer ' + sessionStorage.getItem("token")
        },
        success: function (response) {
            GetStudentCreatePartial(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    });
}

function GetUpdateStudent(id) {
    var myUrl = baseUrl + "Students/" + id;
    $.ajax({
        url: myUrl,
        type: "GET",
        headers: {
            "Authorization": 'Bearer ' + sessionStorage.getItem("token")
        },
        success: function (response) {
            var myUrl1 = baseUrl + "Schools";
            $.ajax({
                url: myUrl1,
                type: "GET",
                headers: {
                    "Authorization": 'Bearer ' + sessionStorage.getItem("token")
                },
                success: function (schools) {
                    GetStudentUpdatePartial(response, schools);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(xhr.responseText);
                }
            });
            /*GetStudentUpdatePartial(response);*/
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    });
}
//function GetStudentUpdatePartial(students) {
//    var myUrl = "/Students/GetUpdateStudentPartial";
//    $.ajax({
//        url: myUrl,
//        type: "POST",
//        data: students,
//        success: function (response) {
//            // kısmi görünümü yükle
//            $("#letMeSee").html(response);

//            // okulları almak için API isteği yap
//            $.ajax({
//                url: baseUrl + "Schools",
//                type: "GET",
//                headers: {
//                    "Authorization": 'Bearer ' + sessionStorage.getItem("token")
//                },
//                success: function (schools) {
//                    // okul seçim alanı elementi
//                    var schoolSelect = $("#SchoolId");

//                    // önceki seçenekleri temizle
//                    schoolSelect.empty();

//                    // her okul için bir seçenek oluştur
//                    $.each(schools, function (index, school) {
//                        var option = $("<option>")
//                            .val(school.id)
//                            .text(school.name);

//                        // eğer öğrencinin okulu bu okula eşitse, seçili olarak belirle
//                        if (school.id == students.schoolId) {
//                            option.attr("selected", true);
//                        }

//                        // seçenekleri ekle
//                        schoolSelect.append(option);
//                    });
//                },
//                error: function (xhr, ajaxOptions, thrownError) {
//                    alert(xhr.status);
//                    alert(xhr.responseText);
//                }
//            });
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            alert(xhr.status);
//            alert(xhr.responseText);
//        }
//    });
//}

function GetStudentUpdatePartial(students, schools) {

    //var schoolIndexVM = { Schools: schools };
    //var dataToSend = {
    //    studentUpdateDto: students,
    //    schoolIndexVM: schoolIndexVM
    //};
    var myUrl = "/Students/GetUpdateStudentPartial";

    /*var dataToSend = { "studentUpdateDto": students, "schoolIndexVM": schools };*/
    $.ajax({
        url: myUrl,
        type: "POST",
        data: { 'studentUpdateDto': students, 'schoolIndexVM': schools },
        success: function (response) {
            $("#letMeSee").html(response);
        }, error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    });
}

function GetStudentCreatePartial(schools) {
    var myUrl = "/Students/GetStudentCreatePartial";
    $.ajax({
        url: myUrl,
        type: "POST",
        data: schools,
        success: function (response) {
            $("#letMeSee").html(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    });
}

function UpdateStudent() {
    let formData = new FormData();
    let input = document.getElementById("Photo");
    let files = input.files;
    formData.append('id', $("#Id").val());
    formData.append('photo', files[0]);
    formData.append('firstName', $("#FirstName").val());
    formData.append('lastName', $("#LastName").val());
    formData.append('schoolId', $("#SchoolId").val());

    var myUrl = baseUrl + "Students"
    if ($('#updateStudentForm').valid()) {
        $.ajax({
            url: myUrl,
            type: "PUT",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data, textSatus, xhr) {
                if (xhr.status == 200) {
                    GetStudentList();
                }
                else {
                    $("#fail").append('<div class="alert alert-dander" role="alert">Error! Student update failed. </div>');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(xhr.responseText);
            }
        });
    }
}

function CreateStudent() {
    let formData = new FormData();
    let input = document.getElementById("Photo");
    let files = input.files;
    formData.append('photo', files[0]);
    formData.append('firstName', $("#FirstName").val());
    formData.append('lastName', $("#LastName").val());
    formData.append('schoolId', $("#SchoolId").val());

    var myUrl = baseUrl + "Students"
    if ($('#createStudentForm').valid()) {
        $.ajax({
            url: myUrl,
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data, textSatus, xhr) {
                if (xhr.status == 201) {
                    GetStudentList();
                }
                else {
                    $("#fail").append('<div class="alert alert-dander" role="alert">Error! Student addition failed. </div>');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(xhr.responseText);
            }
        });
    }
}

/*
/*$(document).ready(function () {*/
//var myUrl = baseUrl + "Students"
//$.ajax({
//    type: "GET",
//    url: myUrl,
//    contentType: "application/json",
//    dataType: "json",
//    success: function (data) {
//        $.each(data, function (item) {
//            var rows = "<tr>" +
//                "<td>" + item["firstName"] + "</td>" +
//                "<td>" + item["lastName"] + "</td>" +
//                "<td>" + item["schoolId"].toString() + "</td>" +
//                "<td>" + item["photoPath"] + "</td>" +
//                "</tr>"
//            $("#studentTable").append(rows);
//        });
//    },
//    error: function (data) {
//        error(data.responseText)
//    }
//});


//$(function () {
//    $("#studentsButton").click(function () {
//        var myUrl = baseUrl + "Students"
//        $.ajax({
//            url: myUrl,
//            type: "GET",
//            contentType:"application/json",
//            dataType: "json",
//            success: function (response) {
//                $.each(response, function (i, item) {
//                    var rows = "<tr>" +
//                        "<td id='firstName'>" + item["firstName"] + "</td>" +
//                        "<td id='lastName'>" + item["lastName"] + "</td>" +
//                        "</tr>"
//                    $("#studentTable").append(rows);
//                });
//            },
//            error: function (data) {
//                error(data.responseText)
//            }
//        });
//    });
//});



//ÇALIŞAN*****************************************
//$(document).ready(function () {
//    var myUrl = baseUrl + "Students";
//    $.ajax({
//        type: "GET",
//        url: myUrl,
//        contentType: "application/json",
//        datatype: "json",
//        success: function (response) {
//            $.each(response, function (i, item) {
//                if (item["school"] !== null && item["school"]["Name"] !== null)
//                {
//                    var schoolName = item["school"]["name"]
//                }
//                else {
//                    var schoolName = "Yok"
//                }
//                var rows = "<tr>" +
//                    "<td id='firstName'>" + item["firstName"] + "</td>" +
//                    "<td id='lastName'>" + item["lastName"] + "</td>" +
//                    "<td id='schoolName'>" + schoolName + "</td>" +
//                    "<td><img src='" + item["photoPath"] + "' style='width: 100px; height: 100px;'/></td>" +
//                    "</tr>"
//                $("#studentTable").append(rows);
//            });
//        },
//        error: function (msg) {
//            alert(msg)
//        }
//    });
//})


//$("#studentsButton").click(function () {
//    var myUrl = baseUrl + "Students";
//    $.ajax({
//        type: "GET",
//        url: myUrl,
//        contentType: "application/json",
//        datatype: "json",
//        success: function (response) {
//            $.each(students, function (i, item) {
//                if (item["school"] !== null && item["school"]["Name"] !== null) {
//                    var schoolName = item["school"]["name"]
//                }
//                else {
//                    var schoolName = "Yok"
//                }
//                var rows = "<tr>" +
//                    "<td id='firstName'>" + item["firstName"] + "</td>" +
//                    "<td id='lastName'>" + item["lastName"] + "</td>" +
//                    "<td id='schoolName'>" + schoolName + "</td>" +
//                    "<td><img src='" + item["photoPath"] + "' style='width: 100px; height: 100px;'/></td>" +
//                    "</tr>"
//                $("#studentTable").append(rows);
//            });
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            alert(xhr.status);
//            alert(xhr.responseText);
//        }
//    });
//})


//$("#studentsButton").click(function () {
//    var myUrl = baseUrl + "Students"
//    $.ajax({
//        type: "GET",
//        url: myUrl,
//        dataType: "json",
//        processData: true,
//        contentType: "application/json",
//        success: function (response) {
//            alert(response);
//            $.each(response, function (i, item) {
//                var rows = "<tr>" +
//                    "<td>" + item["firstName"] + "</td>" +
//                    "<td>" + item["lastName"] + "</td>" +
//                    "</tr>"
//                $("#studentTable").append(rows);
//            });
//        },
//        error: function (data) {
//            error(data.responseText)
//        }
//    });
//});

function Register() {
    let formdata = new FormData();
    formdata.append('userName', $('#userName').val());
    formdata.append('firstName', $('#firstName').val());
    formdata.append('lastName', $('#lastName').val());
    formdata.append('email', $('#email').val());
    formdata.append('password', $('#password').val());
    formdata.append('passwordConfirm', $('#passwordConfirm').val());
    formdata.append('applicationUserRole', $('#applicationUserRole').val());

    var myUrl = baseUrl + "ApplicationUsers";
    if ($('#registerForm').valid()) {
        $.ajax({
            url: myUrl,
            type: "POST",
            data: formdata,
            processData: false,
            contentType: false,
            success: function (response) {
                alert("Registration successful. Please login")
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(xhr.responseText);
            }
        });
    }
}

//$("#loginForm").submit(function (event) {
//    event.preventDefault();

//    var formData = {
//        "email": $("email").val(),
//        "password": $("password").val()
//        //Email: $("#email").val(),
//        //Password: $("#password").val()
//    };
//    var myUrl = baseUrl + "ApplicationUsers/Login"
//    $.ajax({
//        type: "POST",
//        url: myUrl,
//        data: formData,
//        success: function (response) {
//            alert(response);
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//                alert(xhr.status);
//                alert(xhr.responseText);
//        }
//    });
//});


//function Login() {
//    var formData = {
//        email: $('#email').val(),
//        password: $('#password').val()
//    };

//    var myUrl = baseUrl + "ApplicationUsers/Login";
//    if ($('#loginForm').valid()) {
//        $.ajax({
//            url: myUrl,
//            type: "POST",
//            data: formdata,
//            processData: false,
//            contentType: false,
//            success: function (response) {
//                alert("Registration successful. Please login")
//            },
//            error: function (xhr, ajaxOptions, thrownError) {
//                alert(xhr.status);
//                alert(xhr.responseText);
//            }
//        });
//    }
//}

//2802DERSTE
function Login() {
    let formdata = new FormData();
    formdata.append('email', $('#email').val());
    formdata.append('password', $('#password').val());

    var myUrl = baseUrl + 'ApplicationUsers/Login';

    if ($('#loginForm').valid()) {
        $.ajax({
            url: myUrl,
            type: "POST",
            data: formdata,
            processData: false,
            contentType: false,
            success: function (response) {
                sessionStorage.setItem("token", response);
                alert("Welcome");
                GetLoggedInUserRole();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(xhr.responseText);
            }
        })
    }
}

function Logout() {
    var myUrl = "/ApplicationUsers/Logout";

    $.ajax({
        url: myUrl,
        type: "POST",
        headers: {
            "Authorization": 'Bearer ' + sessionStorage.getItem("token")
        },
        success: function () {
            sessionStorage.removeItem("token");
            sessionStorage.removeItem("role");
            localStorage.clear();
            alert("Logged out successfully.");
            GoToHomeIndex();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    })
}

function GetLoggedInUserRole() {
    var myUrl = baseUrl + 'ApplicationUsers';
    $.ajax({
        url: myUrl,
        type: "GET",
        headers: {
            "Authorization": 'Bearer ' + sessionStorage.getItem("token")
        },
        success: function (response) {
            sessionStorage.setItem("role", response);
            GoToHomeIndex();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    })
}

function GoToHomeIndex() {
    window.location.href = "/Home/Index";
}
//  DERSTEKİ BİTTİ**********************




//ÇALIŞAN-benim
//function Login() {
//    var formData = {
//        email: $("#email").val(),
//        password: $("#password").val()
//    };
//    var myUrl = baseUrl + "ApplicationUsers/Login"
//    if ($('#loginForm').valid()) {
//        $.ajax({
//            url: myUrl,
//            type: "POST",
//            data: JSON.stringify(formData),
//            dataType: "json",
//            processData: true,
//            contentType: "application/json",
//            success: function (response) {
//                alert("alooo")
//                window.location == 'https://localhost:7241/Home/Index'
//            },
//            error: function (xhr, ajaxOptions, thrownError) {
//                alert(xhr.status);
//                alert(xhr.responseText);
//            }
//        });
//    }
//}


//function Login() {
//    var formData = {
//        email: $("#email").val(),
//        password: $("#password").val()
//    };
//    var myUrl = baseUrl + "ApplicationUsers/Login"
//    if ($('#loginForm').valid()) {
//        $.ajax({
//            type: "POST",
//            url: myUrl,
//            data: JSON.stringify(formData)
//            dataType: "json",
//            /* jQuery tarafından otomatik olarak işlenir ve geçerli bir sorgu dizgisine (query string) dönüştürülür. Bu işlem, özellikle verilerin URL kodlaması ve karakter kaçırma gibi konularında sorun yaşanmaması için gereklidir. Örneğin, bir formdan gönderilen veriler, jQuery tarafından otomatik olarak işlenerek bir sorgu dizgisine dönüştürülür ve bu sorgu dizgisi, AJAX isteği sırasında sunucuya gönderilir.*/
//            processData: false,
//            contentType: "application/json",
//            success: function (response) {
//                if (response.getItem("token") != null) {
//                    window.location = "https://localhost:7241/Home/Index",
//                    $('#hello').show();

//                }

//            },
//            error: function (xhr, ajaxOptions, thrownError) {
//                alert(xhr.status);
//                alert(xhr.responseText);
//            }
//        });
//    }
//}
//function Login() {
//    var formData = {
//        email: $("#email").val(),
//        password: $("#password").val()
//    };
//    var myUrl = baseUrl + "ApplicationUsers/Login"
//    if ($('#loginForm').valid()) {
//        $.ajax({
//            url: myUrl,
//            type: "POST",
//            data: JSON.stringify(formData),
//            dataType: "json",
//            processData: true,
//            contentType: "application/json",
//        })
//            .done(function (response) {
//                if (response.status == 200) {
//                    alert("Başarılı");
//                } else {
//                    alert("Başarısız");
//                }
//            })
//            .fail(function (xhr, ajaxOptions, thrownError) {
//                alert(xhr.status);
//                alert(xhr.responseText);
//            });
//    }
//}


//function Login() {
//    let formdata = {
//        Email: $('#email').val(),
//        Password: $('#password').val()
//    };
//    var myUrl = baseUrl + "Login"
//    $.ajax({
//        type: "POST",
//        url: myUrl,
//        data: JSON.stringify(formdata),
//        contentType: "application/json",
//        success: function (response) {
//            alert(response),
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            alert(xhr.status);
//            alert(xhr.responseText);
//        }
//    });
//}

function DeleteStudent(id) {
    var myUrl = baseUrl + "Students/" + id;
    $.ajax({
        url: myUrl,
        type: "DELETE",
        success: function (data, textSatus, xhr) {
            if (xhr.status == 200) {
                GetStudentList();
            }
            else {
                $("#fail").append('<div class="alert alert-dander" role="alert">Error! Student update failed. </div>');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
        }
    });
}