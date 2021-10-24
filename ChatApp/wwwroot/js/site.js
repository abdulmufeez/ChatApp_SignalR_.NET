var createRoomBtn = document.getElementById('create-room-btn')
var createRoomModal = document.getElementById('create-room-modal')

createRoomBtn.addEventListener('click', function(e) {           //add an event when click and calls a function
     createRoomModal.classList.add('active')                    //this will add active class to the room-modal
})

var closeModal = function(){
    createRoomModal.classList.remove('active')
}