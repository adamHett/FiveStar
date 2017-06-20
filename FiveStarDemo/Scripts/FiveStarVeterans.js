//Functions are JQUERY JS


//Getting Applicant Information to prepopulate Resident Page
//inout fields
$("#AppInfo_Id").change(function () {
    $.ajax({
        type: "Get",
        url: '/FiveStarResidentInformation/GetResData',
        data: { ID: $("#AppInfo_Id").val() },
        dataType: "json",
        success: function (data) {
            $("#ResInfo_Fname").val(data[0].AppInfo_Fname);
            $("#ResInfo_Lname").val(data[0].AppInfo_Lname);
            $("#ResInfo_SSN").val(data[0].AppInfo_SSN);
            $("#ResInfo_DOB").val(ToJavaScriptDate(data[0].AppInfo_DOB));
            $("#ResInfo_Age").val(data[0].AppInfo_Age);
            $("#ResInfo_Phone").val(data[0].AppInfo_Phone);
            $("#ResInfo_Gender").val(data[0].AppInfo_Gender);
        }
    });
})

//Getting Applicant Information to prepopulate Applicant Interview Page
//input fields
$("#AppInfo_Id").change(function () {
    $.ajax({
        type: "Get",
        url: '/FiveStarApplicantInterviewInformation/GetAppIntData',
        data: { ID: $("#AppInfo_Id").val() },
        dataType: "json",
        success: function (data) {
            $("#AppIntInfo_Fname").val(data[0].AppInfo_Fname);
            $("#AppIntInfo_Lname").val(data[0].AppInfo_Lname);
            $("#AppIntInfo_SSN").val(data[0].AppInfo_SSN);
            $("#AppIntInfo_DOB").val(ToJavaScriptDate(data[0].AppInfo_DOB));
            $("#AppIntInfo_Age").val(data[0].AppInfo_Age);
        }
    });
})

//Converts JSON Server Side Date "/Date(145801440000)/"
//To "Short" Formatted Date
function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
}

