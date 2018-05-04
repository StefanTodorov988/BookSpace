let rating = function (name, rate) {
    const starTotal = 5;
    const starPercentage = (rate / starTotal) * 100;
    const starPercentageRounded = `${(Math.round(starPercentage / 10) * 10)}%`;
    document.querySelector(`#${name} .stars-inner` ).style.width = starPercentageRounded;
}