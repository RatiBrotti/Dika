const buttonStart = document.querySelector(".start");
const buttonsend = document.querySelector(".send");
let result;
const html5Qrcode = new Html5Qrcode("reader");

let arr = [];
buttonStart.addEventListener("click", (e) => {
    e.preventDefault();
    const qrCodeSuccessCallback = (decodedText, decodedResult) => {
        if (decodedText) {
            document.getElementById("show").style.display = "block";
            document.getElementById("result").textContent = decodedText;
            console.log(decodedText);
            // click.addEventListener("click", (e) => {
            // e.preventDefault();

            // });
            result = decodedText;
            console.log(result);
            html5Qrcode.stop();
        }
    };
    const config = { fps: 10, qrbox: { width: 250, height: 250 } };
    html5Qrcode.start(
        { facingMode: "environment" },
        config,
        qrCodeSuccessCallback
    );
});

buttonsend.addEventListener("click", (e) => {
    e.preventDefault();
    console.log(result);

    axios
        .post("https://jsonplaceholder.typicode.com/users", {
            name: result,
            //   username: username.value,
            //   email: email.value,
        })
        .then((res) => {
            //   handler(res);
            console.log(res);
            // arr.push(res.data.name);
            // console.log(arr);
        })
        .catch((rej) => {
            console.log(rej);
        });

    console.log(arr);
});

// console.log(arr);
// const qrCodeSuccessCallback = (decodedText, decodedResult) => {
//   if (decodedText) {
//     document.getElementById("show").style.display = "block";
//     document.getElementById("result").textContent = decodedText;
//     console.log(decodedText);
//     html5Qrcode.stop();
//   }
// };
// const config = { fps: 10, qrbox: { width: 250, height: 250 } };
// html5Qrcode.start({ facingMode: "environment" }, config, qrCodeSuccessCallback);
