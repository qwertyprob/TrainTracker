function pulseBackground(parrent){
    const allCardsBody = document.querySelectorAll(parrent);

    allCardsBody.forEach(card => {

            if (card.querySelector('.delayed')){
                card.classList.add('pulse-card');
            }
            else if(card.querySelector('.now')){
                card.classList.add('pulse-now-card');

            }
        }

    )
}

function pulseTable(parentSelector){
    document.querySelectorAll(parentSelector).forEach(row => {
        if (row.querySelector('.delayed')) {
            row.classList.add('pulse-card');
        }
    });
}

pulseBackground('.card-body');
