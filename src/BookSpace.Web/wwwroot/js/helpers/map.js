function initMap() {
    var uluru = { lat: 42.6669045, lng: 23.29014589999997 };
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 17,
        center: uluru
    });
    var marker = new google.maps.Marker({
        position: uluru,
        map: map
    });
}