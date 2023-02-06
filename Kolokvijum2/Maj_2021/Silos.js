export class Silos
{
    constructor(naziv, kolicina)
    {
        this.naziv = naziv;
        this.kolicina = kolicina
    }
    crtaj(host)
    {
        var div = this.crtajDiv(host, "silos");

        var p = document.createElement("p");
        p.className = "naziv";
        p.innerHTML = this.naziv
        div.appendChild(p);
        p = document.createElement("p");
        p.className = "kolicina"
        p.innerHTML = `${this.kolicina}t/2000t`;
        div.appendChild(p);

        var silos = this.crtajDiv(div, "sil");
        var kol = this.crtajDiv(silos, "kol");
        kol.style.height = (parseInt(this.kolicina)/2000)*300+"px";
    }
    crtajDiv(host, klasa)
    {
        var div = document.createElement("div");
        div.className = klasa;
        host.appendChild(div);

    return div;
    }
}
