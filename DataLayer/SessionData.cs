﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MandalorianDB.Models;
using MandalorianDB.BusinessLayer;
using System.Collections.ObjectModel;

namespace MandalorianDB.DataLayer 
{
	public class SessionData : ObservableObject
	{
		public static List<Episode> GetEpisodeList()
		{
			return new List<Episode>()
		{
			new Episode(1,1,"Chapter 1: The Mandalorian", 1, new ObservableCollection<string>(){"Mandalorian", "Greef Karga","Armorer","Kuiil", "IG-11", "The Child"}, " Five years after the fall of the Galactic Empire a Mandalorian bounty hunter hands his latest bounty to Greef Karga. Then he accepts an under-the-table commission on the outpost world of Nevarro from an enigmatic client with apparent Imperial connections, directing him to track down and capture an unnamed fifty-year-old target. While the Client is indifferent to the target's well-being, his colleague Dr. Pershing insists the target be brought back alive. The Mandalorian is given a down payment of a single bar of Beskar steel, sacred to his people. He takes it to a covert Mandalorian enclave where an armorer uses it to make him a pauldron. Arriving at the planet of the target's last reported location, the Mandalorian is aided by a vapor farmer named Kuiil. Tired of the chaos that bounty hunters bring to the area, Kuiil leads him to the target's location and departs. Entering the remote and heavily defended encampment, the Mandalorian reluctantly teams up with bounty hunting droid IG-11 to clear the camp and find the quarry: a child of Yoda's species. When IG-11 attempts to kill the infant per its bounty orders, the Mandalorian shoots and destroys the droid, taking the Child alive.", "Dave Filoni", "Jon Favreau" ),
			new Episode(2,2,"Chapter 2: The Child",1,new ObservableCollection<string>(){"Mandalorian", "Kuiil","The Child"},"While returning to his ship with the Child, the Mandalorian fights and kills a group of rival bounty hunters who ambush him. Nearing his ship, he finds it being stripped by Jawas for parts, and violently confronts them. When he tries to attack their sandcrawler, the Jawas stun him and drop him from its roof.The following day, Kuiil helps him locate the Jawas and negotiate for the return of his ship's components. The Mandalorian agrees to retrieve the egg of a rhinoceros-like Mudhorn in exchange for the stolen parts. He enters the Mudhorn's cave only to be hurled out by the angry beast inside, which attacks him repeatedly, damaging his armor. As the Mudhorn rushes in for the kill, the Child uses the Force to levitate the beast, allowing the surprised Mandalorian to stab and kill it.He collects the egg and takes it to the Jawas, who crack it open and eat its yellow insides.With the trade complete, the Mandalorian and Kuiil work together to repair the ship, allowing the Mandalorian to leave the planet with the Child.","Rick Famuyiwa","Jon Favreau"),
			new Episode(3,3,"Chapter 3: The Sin",1,new ObservableCollection<string>(){"Mandalorian", "The Child", "Armorer", "The Client"},"The Mandalorian delivers the Child to the Client on Nevarro and collects the bounty of 20 bars of Beskar steel. Uncharacteristically, the Mandalorian asks about the Client's plans for the Child, but is told that it's none of his concern. He leaves before conflict arises. Returning to the Mandalorian enclave, the Mandalorian has his damaged armor replaced and weapons upgraded by the Armorer, who forges a full cuirass from most of the Beskar steel, while the remainder goes to support Mandalorian foundling children. The Mandalorian accepts a new job from Greef Karga and prepares his ship to depart. Feeling guilty for abandoning the Child to the Empire, he turns back to attack the Client's base, killing most of the stormtroopers guarding it. He rescues the Child from Dr. Pershing's laboratory where it was being experimented on, but chooses not to kill the doctor. On the way back to his ship, the Mandalorian is ambushed by other bounty hunters and Greef Karga, who demand that he hand over the Child. He refuses, and a firefight breaks out. Outnumbered and cornered, the Mandalorian is able to escape only when other Mandalorians arrive from the enclave, attacking the bounty hunters and allowing him to reach his ship with the Child.","Deborah Chow","Jon Favreau"),
			new Episode(4,4,"Chapter 4: Sanctuary",1,new ObservableCollection<string>(){"Mandalorian", "Cara Dune","The Child"},"Arriving on the sparsely populated forest planet Sorgan, the Mandalorian encounters ex-Rebel shock trooper-turned-mercenary Cara Dune. Following a short brawl, Dune explains that she is hiding after taking 'early retirement' and asks the Mandalorian to leave. While he prepares his ship, two desperate fishermen approach, offering to hire him to drive off a band of Klatoonian raiders. He accepts the job in exchange for lodging, using their credits to enlist Dune's help. At the village, they are housed by Omera, a widowed mother. The Mandalorian confides in her that no one has seen him without his helmet since childhood when his tribe took him in as an orphan. Despite discovering that the raiders have an old Imperial AT-ST, the villagers refuse to leave, so the Mandalorian and Dune train them to defend themselves. They provoke the raiders at night, with Dune luring the AT-ST into a trap for the Mandalorian to blow up and forcing the remaining raiders to flee. With peace restored, the Mandalorian plans to leave the Child in the village, but a Guild bounty hunter tracks it down and is killed by Dune. Realizing that neither the village nor the Child would be safe, the Mandalorian departs with the Child.","Bryce Dallas Howard","Jon Favreau"),
			new Episode(5,5,"Chapter 5: The Gunslinger",1,new ObservableCollection<string>(){"Mandalorian", "The Child", "Peli Motto", "Toro Calican", "Fennec Shan" },"The Mandalorian defeats a pursuing bounty hunter in a dogfight. He lands his damaged ship at a nearby repair dock, run by mechanic Peli Motto in Mos Eisley on Tatooine. He seeks work in a cantina to pay for the repairs, meeting aspiring bounty hunter Toro Calican, who is tracking elite mercenary and assassin Fennec Shand. Calican needs to catch Shand to join the Guild, and the Mandalorian agrees to help when Calican offers to let him keep the money. They capture Shand in the desert, but she destroys one of their speeder bikes, so the Mandalorian goes to get a dewback they passed for transportation. While Calican watches Shand, she tells him that the Mandalorian betrayed the guild, making the bounty on him and the Child worth more than hers. Shand offers to help Calican capture the Mandalorian if he sets her free, but he shoots her instead and rides the remaining speeder bike to the repair dock, taking Motto and the Child hostage. The Mandalorian arrives, uses a flare to disorient Calican, and kills him. He takes Calican's money to pay Motto for the repairs, thanking her before leaving Tatooine. Out in the desert, a mysterious figure approaches Shand's body.","Dave Filoni","Dave Filoni"),
			new Episode(6,6,"Chapter 6: The Prisoner",1,new ObservableCollection<string>(){"Mandalorian", "The Child", "Ran Malk", "Mayfeld", "Burg", "Q9-0", "Xi'an", "Qin"},"The Mandalorian contacts his former partner 'Ran' Malk for work. Ran welcomes him to his space station and informs the Mandalorian that he needs his ship for a five-man job. He is joined by ex-Imperial sharpshooter Mayfeld, Devaronian strongman Burg, droid pilot Q9-0, and Twi'lek knife-expert Xi'an for a mission to rescue Xi'an's brother Qin, a prisoner of the New Republic. After infiltrating the prison ship, they fight through security droids and make it to the control room where the ship's security chief triggers a security beacon before being killed by Xi'an. The crew rescues Qin but double-crosses the Mandalorian. He escapes and defeats each crew member, then captures Qin. Q9-0 finds the Child after deciphering the archived transmission from Greef Karga but is shot by the Mandalorian before he can harm him. The Mandalorian delivers Qin to Ran and departs with his payment. Ran immediately moves to launch a fighter to kill the Mandalorian but discovers the New Republic beacon had been placed on Qin, leading a trio of X-wings to Ran's station where they attack. In the final scene, Mayfeld, Burg, and Xi'an are revealed to be locked in a cell on the prison transport, having been spared by the Mandalorian.","Rick Famuyiwa", "Christopher Yost"),
			new Episode(7,7,"Chapter 7: The Reckoning",1,new ObservableCollection<string>(){"Mandalorian", "Greef Karga","Armorer","Kuiil", "Cara Dune", "The Child"},"The Mandalorian receives a message from Greef Karga, whose town on Nevarro has been overrun by ex-Imperial troops led by the Client. Karga proposes that the Mandalorian use the Child as bait in order to kill the Client and free the town. In return, Karga will square things with the Guild, which would allow the Mandalorian and the Child to live in peace.Sensing a trap, the Mandalorian recruits Cara Dune and Kuiil to assist him, and Kuiil brings a rebuilt and reprogrammed IG - 11 to protect the Child. They meet Karga and his associates but are attacked by flying creatures during the journey to the town. Karga is injured, but the Child uses the Force to heal his wound. In return, Karga kills his associates and confesses his original plan to shoot the Mandalorian and take the Child to the Client. Karga pretends that Dune has captured the Mandalorian, while Kuiil returns the Child to the ship. During the meeting, Moff Gideon's troops open fire on the building and kill the Client and his bodyguards, trapping the Mandalorian, Karga, and Dune inside. Gideon arrives, demanding the Child. In the desert, two scout troopers intercept the Mandalorian's communications and track Kuiil, killing him before he can reach the ship and taking the Child.","Deborah Chow","Jon Favreau"),
			new Episode(8,8,"Chapter 8: Redemption",1,new ObservableCollection<string>(){"Mandalorian", "Greef Karga","Armorer","Kuiil", "IG-11", "The Child","Cara Dune", "Moff Gideon"},"IG-11 rescues the Child from the scout troopers. Gideon warns Karga, Dune, and the Mandalorian that they face certain death unless they agree to assist him. IG-11 arrives and breaks the standoff but Gideon injures the Mandalorian. The Child uses the Force to deflect an attacking stormtrooper's flamethrower back on him. The Mandalorian sends the others through a sewer grate with the Child to find help from the Mandalorian enclave, while IG-11 removes his helmet to tend to a head injury. Joining the others, the Mandalorian finds the covert group of Mandalorians dead or escaped, except for the Armorer. She tasks him to care for the foundling Child like his own, discover its origins, and return it to its kind. The Armorer fashions the Mandalorian his own signet and gives him a jetpack. The group is ferried down an underground lava river, but when they are ambushed by stormtroopers, IG-11 self-destructs to eliminate the enemy. Gideon attacks in a TIE fighter and the Mandalorian uses the jetpack to bring the craft down, but the Moff survives and cuts himself out of the ship with the Darksaber.[a] The Mandalorian leaves with the Child, while Karga and Dune stay behind.","Taika Waititi","Jon Favreau")
		};

		}
	}
}

