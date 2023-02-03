import { Fakultet } from "./Fakultet.js";
import { Predmet } from "./Predmet.js";


var listaPredmeta = [];
var listaRokova = [];
fetch("https://localhost:5001/Predmet/PreuzmiPredmete").then(x => {
    x.json().then(predmeti => {
        predmeti.forEach(predmet => {
            var p = new Predmet(predmet.id, predmet.naziv);
            listaPredmeta.push(p);
        });
        
        fetch("https://localhost:5001/Ispit/IspitniRokovi").then(x => {
        x.json().then(rokovi => {
            rokovi.forEach(rok => {
                var p = new Predmet(rok.id, rok.naziv);
                listaRokova.push(p);
            });
            var f = new Fakultet(listaPredmeta, listaRokova);
            f.crtaj(document.body);
            })
        })
    })
})
console.log(listaPredmeta);


console.log(listaRokova);




