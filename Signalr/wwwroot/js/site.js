"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/RefreshHub").build();

connection.on("LoadProducts", function () {
    console.log("polączenie");
    LoadProdData();
});

connection.start().then(function () {
    LoadProdData();
}).catch(function (err) {
    return console.error(err.toString());
});


function LoadProdData() {
    var tr = '';
    $.ajax({
        url: '/Zamowienia/GetProducts',
        method: 'GET',
        success: (result) => {
            $.each(result, (k, v) => {
                tr += `<tr>
                                <td>${v.Za_Nr_Zamowienia}</td>
                                <td>${v.Za_Nazwa}</td>
                                <td>${v.Za_Odbiorca}</td>
                                <td>
                                <a href='../Zamowienia/Edit/${v.Za_Id}'>Edit</a>
                                <a href='../Zamowienia/Details/${v.Za_Id}'>Details</a>
                                <a href='../Zamowienia/Delete/${v.Za_Id}'>Delete</a>
                               </td>
                        </tr>`
            })

            $("#tableBody").html(tr);

        },
        error: (error) => {
            console.log(error)
        }

    });
}

