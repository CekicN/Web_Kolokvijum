export class biljka
{
    constructor(id, kolicina, naziv)
    {
        this.id = id;
        this.kolicina = kolicina;
        this.naziv = naziv;
    }

    crtaj(host)
    {
        var div = document.createElement("div");
        div.className = "biljka";
        host.appendChild(div);

        var slika = document.createElement("div");
        slika.className = "slika";
        slika.onclick = (ev) => this.klik(ev);
        div.appendChild(slika);

        var p = document.createElement("p");
        p.innerHTML = this.naziv;
        div.appendChild(p);

        p = document.createElement("p");
        p.innerHTML = this.kolicina;
        p.className = "kol";
        div.appendChild(p);
    }
    klik(ev)
    {

        fetch("https://localhost:7086/Controller/PromeniKolicinu/" + this.id, {method:"PUT"}).then(p => 
        {
            p.json().then(q => 
            {
                var kliknut = ev.srcElement.parentElement;
                var z = kliknut.querySelector(".kol");

                z.innerHTML = q.kolicina;
            })
        })
    }
}