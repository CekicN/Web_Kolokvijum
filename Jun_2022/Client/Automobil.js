export class Automobil{
    constructor(marka, model, slika, kolicina, datum, cena) {
        this.marka = marka;
        this.model = model;
        this.slika= slika;
        this.kolicina = kolicina;
        this.datum = datum;
        this.cena = cena;
    }

    crtaj(host)
    {
        var div = document.createElement("div");
        div.className = "automobil";
        host.appendChild(div);
        
        var ma = document.createElement("p");
        ma.innerHTML = `Marka: ${this.marka}`;
        ma.className = "marka";
        div.appendChild(ma);
        
        ma = document.createElement("p");
        ma.innerHTML = `Model: ${this.model}`;
        ma.className = "model";
        div.appendChild(ma);

        ma = document.createElement("h1");
        ma.innerHTML = `SLIKA: ${this.slika}`;
        div.appendChild(ma);

        ma = document.createElement("p");
        ma.innerHTML = `Kolicina: ${this.kolicina}`;
        ma.className = "kolicina";
        div.appendChild(ma);

        ma = document.createElement("p");
        ma.innerHTML = `Datum poslednje prodaje: ${this.datum}`;
        div.appendChild(ma);

        ma = document.createElement("p");
        ma.innerHTML = `Cena: ${this.cena}`;
        div.appendChild(ma);

        ma = document.createElement("button");
        ma.innerHTML = `Naruci`;
        ma.onclick = (ev) => this.NaruciAutomobil(ev);
        div.appendChild(ma);
    }

    NaruciAutomobil(ev)
    {
        var kliknut = ev.srcElement;

        var ma = kliknut.closest("div").querySelector(".marka").innerHTML;
        var mo = kliknut.closest("div").querySelector(".model").innerHTML; 
        var kol = kliknut.closest("div").querySelector(".kolicina");
        var marka = ma.substr(ma.indexOf(':') + 1).trim();
        var model = mo.substr(mo.indexOf(':') + 1).trim();
        console.log(marka);
        console.log(model);
        fetch(`http://localhost:5270/Automobil/NaruciAutomobil/${marka}/${model}`, {method:"PUT",headers:{"Content-Type":"application/json"}})
        .then(p => {p.json()
            .then(auto => {
                var a = new Automobil(auto.marka, auto.model, auto.slika, auto.kolicina, auto.datum, auto.cena);
                
                if(a.kolicina < 1)
                {
                    var parent = document.querySelector(".rezultat");

                    parent.removeChild(kliknut.closest("div"));
                }
                else
                {
                    kol.innerHTML = `Kolicina: ${a.kolicina}`;
                }
            })
        })

    }
}