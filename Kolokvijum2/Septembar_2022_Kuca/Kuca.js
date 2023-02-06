export class Kuca
{
    constructor(materijal, fasada, stolarija, krov)
    {
        this.materijal = materijal;
        this.fasada = fasada;
        this.stolarija = stolarija;
        this.krov = krov;
    }
    crtaj(host)
    {
        this.brisi();
        var kuca = this.crtajDiv(host, "kuca");
        //krov
        var krov = this.crtajDiv(kuca, "krov");
        krov.style.border = "105px solid transparent"
        krov.style.borderBottom = `105px solid ${this.krov}`;
        //zid
        var zid = this.crtajDiv(kuca, "zid");
        zid.style.backgroundColor = this.materijal;

        zid.style.border = "5px solid "+this.fasada;
        console.log("5px solid"+this.fasada)
        //prozori
        var prozori = this.crtajDiv(zid, "prozori");

        var prozor1 = this.crtajDiv(prozori, "p1");
        krov = this.crtajDiv(prozor1, "l1");
        var prozor2 = this.crtajDiv(prozori, "p1");
        krov = this.crtajDiv(prozor2, "l1");
        prozor1.style.backgroundColor = this.stolarija
        prozor2.style.backgroundColor = this.stolarija
        //vrata
        var vrata = this.crtajDiv(zid, "vrata");
        vrata.style.backgroundColor = this.stolarija;

        

    }

    crtajDiv(host, klasa)
    {
        var glavniDiv = document.createElement("div");
        glavniDiv.className = klasa;
        host.appendChild(glavniDiv);  
        return glavniDiv;     
    }

    brisi()
    {
        var rez = document.querySelector(".rezultat");
        var kuca = document.querySelector(".kuca");

        if(kuca != null)
        {
            rez.removeChild(kuca);
        }
            
    }
}