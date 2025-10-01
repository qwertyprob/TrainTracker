function toggleShow() {
    const tableBtn = document.getElementById('tableBtn');
    const cardsBtn = document.getElementById('cardsBtn');

    const tableRow = document.getElementById('tableRow');
    const cardsRow = document.getElementById('cardsRow');

    const tableTxt = document.getElementById('tableTxt');
    const cardsTxt = document.getElementById('cardsTxt');

    tableBtn.addEventListener('click', function () {
        tableRow.classList.add('visible');
        tableRow.classList.remove('hidden');

        cardsRow.classList.add('hidden');
        cardsRow.classList.remove('visible');

        tableTxt.classList.remove('hidden');
        cardsTxt.classList.add('hidden');

        tableBtn.classList.add('active');
        cardsBtn.classList.remove('active');
    });

    cardsBtn.addEventListener('click', function () {
        cardsRow.classList.add('visible');
        cardsRow.classList.remove('hidden');

        tableRow.classList.add('hidden');
        tableRow.classList.remove('visible');

        cardsTxt.classList.remove('hidden');
        tableTxt.classList.add('hidden');

        cardsBtn.classList.add('active');
        tableBtn.classList.remove('active');
    });
}

toggleShow();
