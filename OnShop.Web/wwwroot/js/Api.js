var Api = {
    DeleteItem: function ( removeConfig) {
        $.ajax({
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json',
            type: "DELETE",
            url: removeConfig.url,
            data: { id: removeConfig.id },
            success: function (data) {
                if (data.IsSuccess === true) {
                    removeConfig.success(data)
                    
                }
                else {
                    removeConfig.warning(data)
                }
            },
            error: function (request, status, error) {
                removeConfig.warning(request.responseText)
            }

        });
    }
}

var Ui = {
    DeleteArrangement: function (id) {
        Ui._DeleteItem("/Admin/Arrangement/Delete", id)
    },
    DeleteNotification: function (id) {
        Ui._DeleteItem("/Admin/Notification/Delete", id)
    },
    DeletePost: function (id) {
        Ui._DeleteItem("/Admin/Post/Delete", id)
    },
    DeleteSlider: function (id) {
        Ui._DeleteItem("/Admin/Slider/Delete", id)
    },
    DeletePostCategory: function (id) {
        Ui._DeleteItem("/Admin/PostCategory/Delete", id)
    },
    ClearNotification: function () {
        Ui._DeleteItem("/Admin/Notification/Clear")
    },
    _DeleteItem: function (url, id) {
        Swal.fire({
            title: 'حذف!',
            text: "آیا از حذف اطمینان دارید؟!",
            icon: 'error',
            showCancelButton: true,
            confirmButtonColor: '#28a745',
            cancelButtonColor: '#d33',
            confirmButtonText: 'بله',
            cancelButtonText: 'خیر',
            reverseButtons: true

        }).then((result) => {
            if (result.isConfirmed) {
                Api.DeleteItem({
                    id: id,
                    url: url,
                    success: function (data) {
                        console.log(data);
                        Swal.fire(
                            'موفق!',
                            `${data.Message ?? ''}`,
                            'success'
                        ).then(function (isConfirm) {
                            location.reload();
                        });
                    },
                    warning: function (data) {
                        Swal.fire(
                            'هشدار!',
                            data.Message,
                            'warning'
                        );
                    },
                    error: function (data) {
                        Swal.fire(
                            'هشدار!',
                            data,
                            'error'
                        );
                    }

                })


            }
        });
    },

    LoadNotification: function () {
        $.ajax({
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json',
            type: "POST",
            url: "/Admin/Notification/List",
            success: function (data) {
                $("#notificationCount").html(data.recordsTotal);
                $.each(data.data, function (index, value) {

                    if (value.IsRead) {
                        $("#notificationList").append(
                            ` <li class='text-light'>
                    <a href="/Admin/Notification">
                        <i class="fa fa-warning text-warning"></i>${value.Title}
                    </a>
                </li>`
                        );
                    }
                    else {
                        $("#notificationList").append(
                            ` <li class='text-dark'>
                    <a href="/Admin/Notification" >
                        <i class="fa fa-warning text-warning"></i><b>${value.Title}</b>
                    </a>
                </li>`
                        );
                    }

                });


            }
        });
    },

    MarkAsReadNotification: function (id) {
        $.ajax({
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json',
            type: "PUT",
            url: "/Admin/Notification/MarkAsRead",
            data: { id: id },
            success: function (data) {
                location.reload();

            }
        });
    },

    Grid : {
        Bool:function (data) {
            if (data)
                return "<i class='fa fa-check text-info'></i>";
            else
                return "<i class='fa fa-remove text-warning'></i>";
        },
    }
}

