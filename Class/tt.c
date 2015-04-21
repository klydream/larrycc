actions=use_item,name=shards_of_nothing
actions+=/potion,name=draenic_intellect,if=buff.bloodlust.remains>30|(((buff.dark_soul.up&(trinket.proc.any.react|trinket.stacking_proc.any.react>6)&!buff.demonbolt.remains)|target.health.pct<20)&(!talent.grimoire_of_service.enabled|!talent.demonic_servitude.enabled|pet.service_doomguard.active))
actions+=/berserking
actions+=/blood_fury
actions+=/arcane_torrent
actions+=/mannoroths_fury
actions+=/dark_soul,if=talent.demonbolt.enabled&((charges=2&((!glyph.imp_swarm.enabled&(dot.corruption.ticking|trinket.proc.haste.remains<=10))|cooldown.imp_swarm.remains))|target.time_to_die<buff.demonbolt.remains|(!buff.demonbolt.remains&demonic_fury>=790))
actions+=/dark_soul,if=!talent.demonbolt.enabled&((charges=2&(time>6|(debuff.shadowflame.stack=1&action.hand_of_guldan.in_flight)))|!talent.archimondes_darkness.enabled|(target.time_to_die<=20&!glyph.dark_soul.enabled)|target.time_to_die<=10|(target.time_to_die<=60&demonic_fury>400)|((trinket.proc.any.react|trinket.stacking_proc.any.react)&(demonic_fury>600|(glyph.dark_soul.enabled&demonic_fury>450))))
actions+=/imp_swarm,if=!talent.demonbolt.enabled&(buff.dark_soul.up|(cooldown.dark_soul.remains>(120%(1%spell_haste)))|time_to_die<32)&time>3
actions+=/felguard:felstorm
actions+=/wrathguard:wrathstorm
actions+=/wrathguard:mortal_cleave,if=pet.wrathguard.cooldown.wrathstorm.remains>5
actions+=/hand_of_guldan,if=!in_flight&dot.shadowflame.remains<travel_time+action.shadow_bolt.cast_time&(((set_bonus.tier17_4pc=0&((charges=1&recharge_time<4)|charges=2))|(charges=3|(charges=2&recharge_time<13.8-travel_time*2))&((cooldown.cataclysm.remains>dot.shadowflame.duration)|!talent.cataclysm.enabled))|dot.shadowflame.remains>travel_time)
actions+=/hand_of_guldan,if=!in_flight&dot.shadowflame.remains<travel_time+action.shadow_bolt.cast_time&talent.demonbolt.enabled&((set_bonus.tier17_4pc=0&((charges=1&recharge_time<4)|charges=2))|(charges=3|(charges=2&recharge_time<13.8-travel_time*2))|dot.shadowflame.remains>travel_time)
actions+=/hand_of_guldan,if=!in_flight&dot.shadowflame.remains<3.7&time<5&buff.demonbolt.remains<gcd*2&(charges>=2|set_bonus.tier17_4pc=0)&action.dark_soul.charges>=1
actions+=/service_pet,if=talent.grimoire_of_service.enabled&(target.time_to_die>120|target.time_to_die<=25|(buff.dark_soul.remains&target.health.pct<20))
actions+=/summon_doomguard,if=!talent.demonic_servitude.enabled&active_enemies<9
actions+=/summon_infernal,if=!talent.demonic_servitude.enabled&active_enemies>=9
actions+=/call_action_list,name=db,if=talent.demonbolt.enabled
actions+=/kiljaedens_cunning,if=!cooldown.cataclysm.remains&buff.metamorphosis.up
actions+=/cataclysm,if=buff.metamorphosis.up
actions+=/immolation_aura,if=demonic_fury>450&active_enemies>=3&buff.immolation_aura.down
actions+=/doom,if=buff.metamorphosis.up&target.time_to_die>=30*spell_haste&remains<=(duration*0.3)&(remains<cooldown.cataclysm.remains|!talent.cataclysm.enabled)&trinket.stacking_proc.multistrike.react<10
actions+=/corruption,cycle_targets=1,if=target.time_to_die>=6&remains<=(0.3*duration)&buff.metamorphosis.down
actions+=/cancel_metamorphosis,if=buff.metamorphosis.up&((demonic_fury<650&!glyph.dark_soul.enabled)|demonic_fury<450)&buff.dark_soul.down&(trinket.stacking_proc.multistrike.down&trinket.proc.any.down|demonic_fury<(800-cooldown.dark_soul.remains*(10%spell_haste)))&target.time_to_die>20
actions+=/cancel_metamorphosis,if=buff.metamorphosis.up&action.hand_of_guldan.charges>0&dot.shadowflame.remains<action.hand_of_guldan.travel_time+action.shadow_bolt.cast_time&((demonic_fury<100&buff.dark_soul.remains>10)|time<15)&!glyph.dark_soul.enabled
actions+=/cancel_metamorphosis,if=buff.metamorphosis.up&action.hand_of_guldan.charges=3&(!buff.dark_soul.remains>gcd|action.metamorphosis.cooldown<gcd)
actions+=/chaos_wave,if=buff.metamorphosis.up&(buff.dark_soul.up&active_enemies>=2|(charges=3|set_bonus.tier17_4pc=0&charges=2))
actions+=/soul_fire,if=buff.metamorphosis.up&buff.molten_core.react&(buff.dark_soul.remains>execute_time|target.health.pct<=25)&(((buff.molten_core.stack*execute_time>=trinket.stacking_proc.multistrike.remains-1|demonic_fury<=ceil((trinket.stacking_proc.multistrike.remains-buff.molten_core.stack*execute_time)*40)+80*buff.molten_core.stack)
	                                                                                                                                |target.health.pct<=25)
	                                                                                                                                &trinket.stacking_proc.multistrike.remains>=execute_time|trinket.stacking_proc.multistrike.down|!trinket.has_stacking_proc.multistrike)
actions+=/touch_of_chaos,cycle_targets=1,if=buff.metamorphosis.up&dot.corruption.remains<17.4&demonic_fury>750
actions+=/touch_of_chaos,if=buff.metamorphosis.up
actions+=/metamorphosis,if=buff.dark_soul.remains>gcd&(time>6|debuff.shadowflame.stack=2)&(demonic_fury>300|!glyph.dark_soul.enabled)&(demonic_fury>=80&buff.molten_core.stack>=1|demonic_fury>=40)
actions+=/metamorphosis,if=(trinket.stacking_proc.multistrike.react|trinket.proc.any.react)&((demonic_fury>450&action.dark_soul.recharge_time>=10&glyph.dark_soul.enabled)|(demonic_fury>650&cooldown.dark_soul.remains>=10))
actions+=/metamorphosis,if=!cooldown.cataclysm.remains&talent.cataclysm.enabled
actions+=/metamorphosis,if=!dot.doom.ticking&target.time_to_die>=30%(1%spell_haste)&demonic_fury>300
actions+=/metamorphosis,if=(demonic_fury>750&(action.hand_of_guldan.charges=0|(!dot.shadowflame.ticking&!action.hand_of_guldan.in_flight_to_target)))|floor(demonic_fury%80)*action.soul_fire.execute_time>=target.time_to_die
actions+=/metamorphosis,if=demonic_fury>=950
actions+=/cancel_metamorphosis
actions+=/imp_swarm
actions+=/hellfire,interrupt=1,if=active_enemies>=5
actions+=/soul_fire,if=buff.molten_core.react&(buff.molten_core.stack>=7|target.health.pct<=25|(buff.dark_soul.remains&cooldown.metamorphosis.remains>buff.dark_soul.remains)|trinket.proc.any.remains>execute_time|trinket.stacking_proc.multistrike.remains>execute_time)
	                                           &(buff.dark_soul.remains<action.shadow_bolt.cast_time|buff.dark_soul.remains>execute_time)
actions+=/soul_fire,if=buff.molten_core.react&target.time_to_die<(time+target.time_to_die)*0.25+cooldown.dark_soul.remains
actions+=/life_tap,if=mana.pct<40&buff.dark_soul.down
actions+=/hellfire,interrupt=1,if=active_enemies>=4
actions+=/shadow_bolt
actions+=/hellfire,moving=1,interrupt=1
actions+=/life_tap





actions+=/soul_fire,if=buff.molten_core.react&(buff.molten_core.stack>=7|target.health.pct<=25|(buff.dark_soul.remains&cooldown.metamorphosis.remains>buff.dark_soul.remains)|trinket.proc.any.remains>execute_time)&(buff.dark_soul.remains<action.shadow_bolt.cast_time|buff.dark_soul.remains>execute_time)




actions=use_item,name=shards_of_nothing
actions+=/potion,name=draenic_intellect,if=buff.bloodlust.react&buff.dark_soul.remains>10|target.time_to_die<=25|buff.dark_soul.remains>10
actions+=/berserking
actions+=/blood_fury
actions+=/arcane_torrent
actions+=/mannoroths_fury
actions+=/dark_soul,if=!talent.archimondes_darkness.enabled|(talent.archimondes_darkness.enabled&(charges=2|trinket.proc.any.react|trinket.stacking_any.intellect.react>6|target.time_to_die<40))
actions+=/service_pet,if=talent.grimoire_of_service.enabled&(target.time_to_die>120|target.time_to_die<20|(buff.dark_soul.remains&target.health.pct<20))
actions+=/summon_doomguard,if=!talent.demonic_servitude.enabled&active_enemies<9
actions+=/summon_infernal,if=!talent.demonic_servitude.enabled&active_enemies>=9
actions+=/run_action_list,name=single_target,if=active_enemies<6&(!talent.charred_remains.enabled|active_enemies<4)
actions+=/run_action_list,name=aoe,if=active_enemies>=6|(talent.charred_remains.enabled&active_enemies>=4)

actions.single_target=havoc,target=2
actions.single_target+=/shadowburn,if=talent.charred_remains.enabled&target.time_to_die<10
actions.single_target+=/kiljaedens_cunning,if=(talent.cataclysm.enabled&!cooldown.cataclysm.remains)
actions.single_target+=/kiljaedens_cunning,moving=1,if=!talent.cataclysm.enabled
actions.single_target+=/cataclysm,if=active_enemies>1
actions.single_target+=/fire_and_brimstone,if=buff.fire_and_brimstone.down&dot.immolate.remains<=action.immolate.cast_time&(cooldown.cataclysm.remains>action.immolate.cast_time|!talent.cataclysm.enabled)&active_enemies>4
actions.single_target+=/immolate,cycle_targets=1,if=remains<=cast_time&(cooldown.cataclysm.remains>cast_time|!talent.cataclysm.enabled)
actions.single_target+=/cancel_buff,name=fire_and_brimstone,if=buff.fire_and_brimstone.up&dot.immolate.remains>(dot.immolate.duration*0.3)
actions.single_target+=/shadowburn,if=buff.havoc.remains
actions.single_target+=/chaos_bolt,if=buff.havoc.remains>cast_time&buff.havoc.stack>=3
actions.single_target+=/conflagrate,if=charges=2
actions.single_target+=/cataclysm
actions.single_target+=/rain_of_fire,if=remains<=tick_time&(active_enemies>4|(buff.mannoroths_fury.up&active_enemies>2))
actions.single_target+=/chaos_bolt,if=talent.charred_remains.enabled&active_enemies>1&target.health.pct>20
actions.single_target+=/chaos_bolt,if=talent.charred_remains.enabled&buff.backdraft.stack<3&burning_ember>=2.5
actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&(burning_ember>=3.5|buff.dark_soul.up|target.time_to_die<20)
actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&set_bonus.tier17_2pc=1&burning_ember>=2.5      
actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&buff.archmages_greater_incandescence_int.react&buff.archmages_greater_incandescence_int.remains>cast_time
actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&trinket.proc.intellect.react&trinket.proc.intellect.remains>cast_time
actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&trinket.proc.crit.react&trinket.proc.crit.remains>cast_time
actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&trinket.stacking_proc.multistrike.react>=8&trinket.stacking_proc.multistrike.remains>=cast_time
actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&trinket.proc.multistrike.react&trinket.proc.multistrike.remains>cast_time
actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&trinket.proc.versatility.react&trinket.proc.versatility.remains>cast_time
actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&trinket.proc.mastery.react&trinket.proc.mastery.remains>cast_time
actions.single_target+=/fire_and_brimstone,if=buff.fire_and_brimstone.down&dot.immolate.remains<=(dot.immolate.duration*0.3)&active_enemies>4
actions.single_target+=/immolate,cycle_targets=1,if=remains<=(duration*0.3)
actions.single_target+=/conflagrate
actions.single_target+=/incinerate

actions.aoe=rain_of_fire,if=!talent.charred_remains.enabled&remains<=tick_time
actions.aoe+=/havoc,target=2,if=(!talent.charred_remains.enabled|buff.fire_and_brimstone.down)
actions.aoe+=/shadowburn,if=!talent.charred_remains.enabled&buff.havoc.remains
actions.aoe+=/chaos_bolt,if=!talent.charred_remains.enabled&buff.havoc.remains>cast_time&buff.havoc.stack>=3
actions.aoe+=/kiljaedens_cunning,if=(talent.cataclysm.enabled&!cooldown.cataclysm.remains)
actions.aoe+=/kiljaedens_cunning,moving=1,if=!talent.cataclysm.enabled
actions.aoe+=/cataclysm
actions.aoe+=/fire_and_brimstone,if=buff.fire_and_brimstone.down
actions.aoe+=/immolate,if=buff.fire_and_brimstone.up&!dot.immolate.ticking
actions.aoe+=/conflagrate,if=buff.fire_and_brimstone.up&charges=2
actions.aoe+=/immolate,if=buff.fire_and_brimstone.up&dot.immolate.remains<=(dot.immolate.duration*0.3)
actions.aoe+=/chaos_bolt,if=talent.charred_remains.enabled&buff.fire_and_brimstone.up
actions.aoe+=/incinerate

actions+=/soul_fire,if=buff.molten_core.react&(buff.molten_core.stack>=7|target.health.pct<=25|(buff.dark_soul.remains&cooldown.metamorphosis.remains>buff.dark_soul.remains)|trinket.proc.any.remains>execute_time|trinket.stacking_proc.multistrike.remains>execute_time)&(buff.dark_soul.remains<action.shadow_bolt.cast_time|buff.dark_soul.remains>execute_time)

actions+=/soul_fire,if=buff.molten_core.react&(buff.molten_core.stack>=7|target.health.pct<=25|(buff.dark_soul.remains&cooldown.metamorphosis.remains>buff.dark_soul.remains))&(trinket.proc.any.remains>execute_time|trinket.stacking_proc.multistrike.remains>execute_time|(buff.dark_soul.remains<action.shadow_bolt.cast_time&buff.dark_soul.remains>execute_time))
actions+=/soul_fire,if=buff.molten_core.react&buff.molten_core.stack>=8



                if(utils.CanCast(SOUL_FIRE) && utils.isAuraActive(METAMORPHOSIS) 
                  && ((demonic_fury<650 && !HasGlyph(DARK_SOUL)) || demonic_fury<450)
                  && !utils.isAuraActive(DARK_SOUL)
                  && ((!utils.isAuraActive(MARK_OF_BLEEDING_HOLLOW) && !utils.isAuraActive(ARCHMAGES_GREATER_INCANDESCENCE) && !utils.isAuraActive(HOWLING_SOUL)) 
                       || (utils.GetSpellCooldown(DARK_SOUL).Seconds <= 20 && utils.GetCharges(DARK_SOUL)==0 && demonic_fury<300)) && time_to_die>20)
                {
                	  utils.LogActivity("Cancel Metamorphosis for next dark soul"+demonic_fury);
                    Me.GetAuraByName(METAMORPHOSIS).TryCancelAura();
                    return true;
                }
                
                
                if(utils.CanCast(SOUL_FIRE) && utils.isAuraActive(METAMORPHOSIS) && ((demonic_fury<650 && !HasGlyph(DARK_SOUL)) || demonic_fury<450) && !utils.isAuraActive(DARK_SOUL) && time_to_die>20)
                {
                    if(!utils.isAuraActive(MARK_OF_BLEEDING_HOLLOW) && !utils.isAuraActive(ARCHMAGES_GREATER_INCANDESCENCE) && !utils.isAuraActive(HOWLING_SOUL))
                    {    
                        Me.GetAuraByName(METAMORPHOSIS).TryCancelAura();
                        utils.LogActivity("Cancel Metamorphosis for next dark soul with no other buff"+demonic_fury);
                        return true;
                    }
                    else if(utils.GetSpellCooldown(DARK_SOUL).Seconds <= 20 && utils.GetCharges(DARK_SOUL)==0 && demonic_fury<300)
                    {    
                        Me.GetAuraByName(METAMORPHOSIS).TryCancelAura();
                        utils.LogActivity("Cancel Metamorphosis for next dark soul with other buff"+demonic_fury);
                        return true;
                    }
                }
                
                
                
                
                
                
                
                
                
                
                //actions+=/soul_fire,if=buff.molten_core.react&(buff.molten_core.stack>=7|target.health.pct<=25|(buff.dark_soul.remains&cooldown.metamorphosis.remains>buff.dark_soul.remains)|trinket.proc.any.remains>execute_time|trinket.stacking_proc.multistrike.remains>molten_core_execute_time)
                //                                             &(buff.dark_soul.remains<action.shadow_bolt.cast_time|buff.dark_soul.remains>execute_time)
                if (utils.CanCast(SOUL_FIRE) && (utils.GetAuraStack(target, MOLTEN_CORE, true)>=7 || Me.CurrentTarget.HealthPercent<25 || (utils.isAuraActive(DARK_SOUL) && utils.GetSpellCooldown(METAMORPHOSIS).TotalMilliseconds>(int)utils.MyAuraTimeLeft(DARK_SOUL, Me)) || (int)utils.MyAuraTimeLeft(ARCHMAGES_GREATER_INCANDESCENCE, Me)>molten_core_execute_time
                                                                                                                                                                                                                                                                         || (int)utils.MyAuraTimeLeft(HOWLING_SOUL, Me)>molten_core_execute_time
                                                                                                                                                                                                                                                                         || (int)utils.MyAuraTimeLeft(MARK_OF_BLEEDING_HOLLOW, Me)>molten_core_execute_time)
                   && utils.isAuraActive(MOLTEN_CORE) && ((int)utils.MyAuraTimeLeft(DARK_SOUL, Me)<utils.GetSpellCastTime(SHADOW_BOLT).TotalMilliseconds || (int)utils.MyAuraTimeLeft(DARK_SOUL, Me)>molten_core_execute_time))
                {
                    utils.LogActivity(SOUL_FIRE, target.Name);
                    return utils.Cast(SOUL_FIRE, target);
                }