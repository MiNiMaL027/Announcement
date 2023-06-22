function loadAnnouncements() {
    var announcementList = $('#announcementList');
    announcementList.empty();

    $.ajax({
        url: '/Home/GetAllAnnouncementsTitle',
        method: 'GET',
        success: function (data) {
            data.forEach(function (announcement) {
                var listItem = $('<li>').addClass('announcement-list-item').text(announcement.title);
                listItem.click(function () {
                    showAnnouncementDetails(announcement.id);
                });
                announcementList.append(listItem);
            });
        }
    });
}

function showAnnouncementDetails(id) {
    var announcementDetails = $('#announcementDetails');
    announcementDetails.empty();

    $.ajax({
        url: '/Home/GetAnnouncementById',
        method: 'GET',
        data: { id: id },
        success: function (data) {
            announcementDetails.data('announcement-id', data.id);

            var title = $('<h2>').text(data.title);
            var description = $('<p>').text(data.description);

            var titleInput = $('<input>').val(data.title);
            titleInput.blur(function () {
                var updatedTitle = titleInput.val();
                var updatedDescription = descriptionInput.val();
                var announcement = { Title: updatedTitle, Description: updatedDescription };
                updateAnnouncement(announcement, data.id);
                title.text(updatedTitle);
                titleInput.replaceWith(title);
            });

            var descriptionInput = $('<textarea>').text(data.description);
            descriptionInput.blur(function () {
                var updatedTitle = titleInput.val();
                var updatedDescription = descriptionInput.val();
                var announcement = { Title: updatedTitle, Description: updatedDescription };
                updateAnnouncement(announcement, data.id);
                description.text(updatedDescription);
                descriptionInput.replaceWith(description);
            });

            title.click(function () {
                title.replaceWith(titleInput);
                titleInput.focus();
            });

            description.click(function () {
                description.replaceWith(descriptionInput);
                descriptionInput.focus();
            });

            announcementDetails.append(title);
            announcementDetails.append(description);

            var similarAnnouncements = $('#similarAnnouncements');
            similarAnnouncements.empty();

            $.ajax({
                url: '/Home/GetSimilarAnnouncementsTitle',
                method: 'GET',
                data: { id: id },
                success: function (similarData) {
                    similarData.forEach(function (similarAnnouncement) {
                        var similarItem = $('<li>').text(similarAnnouncement.title);
                        similarItem.click(function () {
                            showAnnouncementDetails(similarAnnouncement.id);
                        });
                        similarAnnouncements.append(similarItem);
                    });
                }
            });

            var popup = $('#announcementPopup');
            popup.css('display', 'block');
        }
    });
}

function updateAnnouncement(announcement, id) {
    $.ajax({
        url: '/Home/UpdateAnnouncement',
        method: 'POST',
        data: {
            announcement: announcement,
            id: id
        },
        success: function () {
            loadAnnouncements();
        }
    });
}

function closeAnnouncementPopup() {
    var popup = $('#announcementPopup');
    popup.css('display', 'none');
}

function showAddAnnouncementPopup() {
    var addAnnouncementPopup = $('#addAnnouncementPopup');
    addAnnouncementPopup.css('display', 'block');
}

function closeAddAnnouncementPopup() {
    var addAnnouncementPopup = $('#addAnnouncementPopup');
    addAnnouncementPopup.css('display', 'none');
}

function addAnnouncement() {
    var titleInput = $('#announcementTitle');
    var descriptionInput = $('#announcementDescription');

    $.ajax({
        url: '/Home/AddAnnouncement',
        method: 'POST',
        data: {
            Title: titleInput.val(),
            Description: descriptionInput.val()
        },
        success: function () {
            closeAddAnnouncementPopup();
            titleInput.val('');
            descriptionInput.val('');
            loadAnnouncements();
        }
    });
}

function deleteAnnouncement() {
    var announcementId = $('#announcementDetails').data('announcement-id');

    $.ajax({
        url: '/Home/DeleteAnnouncementById',
        method: 'POST',
        data: { id: announcementId },
        success: function () {
            closeAnnouncementPopup();
            loadAnnouncements();
        }
    });
}

// Load announcements on page load
loadAnnouncements();